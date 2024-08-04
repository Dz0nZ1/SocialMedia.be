using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EMS.Application.Common.Dto.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Application.Common.Dto.Auth;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.Extensions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Configuration;
using SocialMedia.Domain.Entities.User;
using SocialMedia.Infrastructure.Configuration;
using SocialMedia.Infrastructure.Identity;

namespace SocialMedia.Infrastructure.Services;

public class AuthService(ApplicationUserManager userManager, IOptions<JwtConfiguration> jwtOptions, ISmDbContext dbContext, IOptions<AesEncryptionConfiguration> aesEncryptionConfiguration) : IAuthService
{
    private readonly JwtConfiguration _jwtConfiguration = jwtOptions.Value;
    private const string Purpose = "passwordless-auth";
    private const string Provider = "PasswordlessLoginTokenProvider";
    
    public async Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress)
    {
        var user = await userManager.FindByEmailAsync(emailAddress);
        string? validationToken = null;

        if (user is null)
        {
            return new BeginLoginResponseDto(validationToken);
        }

        var token = await userManager.GenerateUserTokenAsync(user, Provider, Purpose);
        var bytes = Encoding.UTF8.GetBytes($"{token}:{emailAddress}");
        validationToken = Convert.ToBase64String(bytes);
        
        //send email with this validation token
        return new BeginLoginResponseDto(validationToken);
        
    }
    public async Task<CompleteLoginResponseDto> CompleteLoginAsync(string validationToken)
    {
        var (userToken, emailAddress) = ExtractValidationToken(validationToken);
        var user = await userManager.FindByEmailAsync(emailAddress);

        if (user is not null)
        {
            var isValid = await userManager.VerifyUserTokenAsync(user,
                Provider,
                Purpose,
                userToken);

            if (!isValid)
                throw new UnauthorizedAccessException();

            await userManager.UpdateSecurityStampAsync(user);

            var authClaims = new List<Claim>();
            var roles = new List<string>();

            var rolesFromDb = await userManager.GetRolesAsync(user);
            
            foreach (var roleFromDb in rolesFromDb)
            {
                roles.Add(roleFromDb);
                authClaims.Add(new Claim(ClaimTypes.Role,
                    roleFromDb));
            }
            
            authClaims.Add(new Claim(ClaimTypes.Name, user.Email!));
            
            //
            // authClaims.AddRange(user.Claims.Select(item => new Claim(item.Type,
            //     item.Value)));

            var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(authClaims));
            var refreshToken = GenerateRefreshToken();
            
            await StoreRefreshTokenAsync(user, refreshToken);
            
            return new CompleteLoginResponseDto(user.Id,user.FirstName, user.LastName, user.Username, user.Email, roles, token, refreshToken);
        }

        throw new UnauthorizedAccessException();
    }
    
    public async Task<CompleteLoginResponseDto> BasicLoginAsync(BasicLoginDto basicLoginDto)
    {
        var user = await userManager.FindByEmailAsync(basicLoginDto.Username) ?? throw new NotFoundException("User not found");
        
        if (user.PasswordHash != null)
        {
            if (!user.PasswordHash.Decrypt(aesEncryptionConfiguration.Value.Key).Equals(basicLoginDto.Password))
            {
                throw new UnauthorizedAccessException();
            }
            await userManager.UpdateSecurityStampAsync(user);
            var authClaims = new List<Claim>();
            var roles = new List<string>();

            var rolesFromDb = await userManager.GetRolesAsync(user);
            
            foreach (var roleFromDb in rolesFromDb)
            {
                roles.Add(roleFromDb);
                authClaims.Add(new Claim(ClaimTypes.Role,
                    roleFromDb));
            }
            
            authClaims.Add(new Claim(ClaimTypes.Name, user.Email!));
            
            var token = new JwtSecurityTokenHandler().WriteToken(GenerateJwtToken(authClaims));
            var refreshToken = GenerateRefreshToken();
            
            await StoreRefreshTokenAsync(user, refreshToken);
            
            return new CompleteLoginResponseDto(user.Id, user.FirstName, user.LastName, user.Username, user.Email, roles, token, refreshToken);
        }
        throw new UnauthorizedAccessException();
    }
    
    
    public async Task LogoutAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
        await RevokeAllUserRefreshTokens(user.Id);
        await userManager.UpdateSecurityStampAsync(user);
    }
    
    
    private static Tuple<string, string> ExtractValidationToken(string token)
    {
        var base64EncodedBytes = Convert.FromBase64String(token);
        var tokenDetails = Encoding.UTF8.GetString(base64EncodedBytes);
        var separatorIndex = tokenDetails.IndexOf(':');

        return new Tuple<string, string>(tokenDetails[..separatorIndex],
            tokenDetails[(separatorIndex + 1)..]);
    }

    private JwtSecurityToken GenerateJwtToken(IEnumerable<Claim> authClaims)
    {
        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!));

        var token = new JwtSecurityToken(issuer: _jwtConfiguration.ValidIssuer,
            audience: _jwtConfiguration.ValidAudience,
            expires: DateTime.Now.AddMinutes(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey,
                SecurityAlgorithms.HmacSha256));

        return token;
    }
    
    
    public async Task<TokenResponse> RefreshTokenAsync(TokenRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.RefreshToken))
        {
            throw new NotFoundException("Request not found");
        }

        var principal = GetPrincipalFromExpiredToken(request.AccessToken);
        var email = principal.Identity?.Name;
        var user = await userManager.FindByEmailAsync(email!);

        if (user == null || !await ValidateRefreshTokenAsync(user, request.RefreshToken))
        {
            throw new NotFoundException("Invalid request");
        }
        
        await RevokeAllUserRefreshTokens(user.Id);

        var newAccessToken = GenerateJwtToken(principal.Claims);
        var newRefreshToken = GenerateRefreshToken();

        await StoreRefreshTokenAsync(user, newRefreshToken);

        return new TokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
            RefreshToken = newRefreshToken
        };
    }
    
    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfiguration.Secret!)),
            ValidateLifetime = false // Ne validiraj trajanje jer je token istekao
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;

        if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private async Task<bool> ValidateRefreshTokenAsync(ApplicationUser user, string refreshToken)
    {
        var storedToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.UserId == user.Id && rt.Token == refreshToken && !rt.IsRevoked);

        if (storedToken == null || storedToken.IsRevoked || storedToken.Expiration < DateTime.UtcNow)
        {
            return false;
        }
        storedToken.IsRevoked = true;
        dbContext.RefreshTokens.Update(storedToken);
        await dbContext.SaveChangesAsync(new CancellationToken());

        return true;
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
    
    private async Task StoreRefreshTokenAsync(ApplicationUser user, string refreshToken)
    {
        var token = new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            Expiration = DateTime.UtcNow.AddDays(7),
            IsRevoked = false
        };
        dbContext.RefreshTokens.Add(token);
        await dbContext.SaveChangesAsync(new CancellationToken());
    }
    
    
    private async Task RevokeAllUserRefreshTokens(string userId)
    {
        var userTokens = await dbContext.RefreshTokens.Where(rt => rt.UserId == userId && !rt.IsRevoked).ToListAsync();
        foreach (var token in userTokens)
        {
            token.IsRevoked = true;
        }
        await dbContext.SaveChangesAsync(new CancellationToken());

        if (userTokens.Count > 20) await CleanUpExpiredTokensAsync();

    }
    
    private async Task CleanUpExpiredTokensAsync()
    {
        var expiredTokens = await dbContext.RefreshTokens.Where(rt => rt.Expiration <= DateTime.UtcNow).ToListAsync();
        dbContext.RefreshTokens.RemoveRange(expiredTokens);
        await dbContext.SaveChangesAsync(new CancellationToken());
    }
    
}