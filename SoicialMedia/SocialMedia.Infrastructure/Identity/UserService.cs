using System.Net;
using Microsoft.EntityFrameworkCore;
using SocialMedia.Application.Common.Constants.User;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.Exceptions;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.User;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.User;
using SocialMedia.Infrastructure.Exceptions;

namespace SocialMedia.Infrastructure.Identity;

public class UserService(ApplicationUserManager userManager) : IUserService
{
    public async Task<UserDetailsDto?> CreateUserAsync(CreateUserDetailsDto userDetails, List<string> roles)
    {
        var userAlreadyExists = await userManager.FindByEmailAsync(userDetails.Email);

        if (userAlreadyExists is not null) throw new UserAlreadyExistsException("User Already Exists");
        
        //TODO: Dodati broj Telefona @Nikola
        
        ApplicationUser user = new()
        {
            FirstName = userDetails.FirstName,
            LastName = userDetails.LastName,
            Username = userDetails.Username,
            UserName = userDetails.Username,
            NormalizedUserName = userDetails.Username.ToUpper(),
            ProfilePictureUrl = userDetails.ProfilePictureUrl,
            Bio = userDetails.Bio,
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            Email = userDetails.Email,
            NormalizedEmail = userDetails.Email.ToUpper(),
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D"),
            ConcurrencyStamp = new Guid().ToString("D"),
            AccessFailedCount = 0
        };

        try
        {
            var result = await userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                throw new AuthException("Could not create a new user", new { Errors = result.Errors.ToList() });
            }

            var rolesResult = await userManager.AddToRolesAsync(user,
                roles.Select(x => x.ToUpper()));

            if (!rolesResult.Succeeded)
            {
                throw new AuthException("Could not add roles to user", new { Errors = rolesResult.Errors.ToList() });
            }

            return user.ToDetailsDto();

        }
        catch (Exception e)
        {
            await userManager.DeleteAsync(user);
            throw new AuthException("Could not create a new user", e);
        }
    }

    public async Task<UserDetailsDto?> GetUserAsync(string userId)
    {
        var user = await userManager.Users.Include(x => x.Posts).Where(x => x.Id.Equals(userId)).FirstOrDefaultAsync() ?? throw new NotFoundException("User not found");
        return user.ToDetailsDto();
    }
    
    public async Task<ApplicationUser?> GetUserEntityAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
    }

    public async Task<UserDetailsDto?> GetUserByEmailAsync(string emailAddress)
    {
       var user = await userManager.FindByEmailAsync(emailAddress) ?? throw new NotFoundException("User not found");
       return user.ToDetailsDto();
    }
    
    public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName) => await userManager.IsInRoleAsync(user, roleName);
    
    public async Task<DeleteResponseDto> DeleteUserAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId) ?? throw new NotFoundException("User not found");
        await userManager.DeleteAsync(user);
        return new DeleteResponseDto(true, HttpStatusCode.OK, UserConstants.UserDeleted, DateTime.Now);
    }

    public async Task<List<UserDetailsDto>> GetAllUsers()
    {
      var users =  await userManager.Users.Include(x => x.Posts).ToListAsync();
      return users.ToDetailsListDto();
    }

    public async Task<UserDetailsDto?> UpdateUserAsync(UpdateUserDetailsDto dto)
    {
        var user = await userManager.FindByIdAsync(dto.UserId) ?? throw new NotFoundException("User not found");
        user.ToEntity(dto);
        await userManager.UpdateAsync(user);
        return user.ToDetailsDto();
    }
}