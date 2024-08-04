using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Constants.Auth;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Infrastructure.Identity;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public string? UserId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public List<string> Roles { get;  set; } = new();
    public bool IsAdministrator { get; set; }


    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;

        UserId = GetClaimValue(ClaimTypes.NameIdentifier);

        var identity = httpContextAccessor.HttpContext?.User.Identity;

        if (identity is not null && identity.IsAuthenticated)
        {
            var roles = GetRoleClaimValues();

            if (roles?.Count > 0)
            {
                Roles.AddRange(roles);
            }

            Email = GetClaimValue(ClaimTypes.Email);
            FirstName = GetClaimValue(ClaimTypes.GivenName);
            LastName = GetClaimValue(ClaimTypes.Surname);
            IsAdministrator = Roles.Contains(AuthorizationConstants.Administrator);
        }

    }



    private string? GetClaimValue(string claimType) =>
        _httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;

    private List<string>? GetRoleClaimValues() => _httpContextAccessor.HttpContext?.User.Claims
        .Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value).ToList();

}