using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Application.Common.interfaces;

public interface IUserService
{
    Task CreateUserAsync(string emailAddress, List<string> roles);
    Task<ApplicationUser?> GetUserAsync(string userId);
    Task<ApplicationUser?> GetUserByEmailAsync(string emailAddress);
    Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);
}