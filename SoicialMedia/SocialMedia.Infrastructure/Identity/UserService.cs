using SocialMedia.Application.Common.interfaces;
using SocialMedia.Domain.Entities;
using SocialMedia.Infrastructure.Exceptions;

namespace SocialMedia.Infrastructure.Identity;

public class UserService(ApplicationUserManager userManager) : IUserService
{
    public async Task CreateUserAsync(string emailAddress, List<string> roles)
    {
        var userAlradyExists = await GetUserByEmailAsync(emailAddress);
        
        if(userAlradyExists is not null) return;

        ApplicationUser user = new()
        {
            Email = emailAddress,
            UserName = emailAddress
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

        }
        catch (Exception e)
        {
            await userManager.DeleteAsync(user);
            throw new AuthException("Could not create a new user", e);
        }
    }

    public async Task<ApplicationUser?> GetUserAsync(string userId) => await userManager.FindByIdAsync(userId);


    public async Task<ApplicationUser?> GetUserByEmailAsync(string emailAddress) => await userManager.FindByEmailAsync(emailAddress);


    public async Task<bool> IsInRoleAsync(ApplicationUser user, string roleName) => await userManager.IsInRoleAsync(user, roleName);
}