using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Application.Common.interfaces;

public interface IUserService
{
    Task<UserDetailsDto?> CreateUserAsync(CreateUserDetailsDto user, List<string> roles);
    Task<UserDetailsDto?> GetUserAsync(string userId);

    Task<ApplicationUser?> GetUserEntityAsync(string userId);
    Task<UserDetailsDto?> GetUserByEmailAsync(string emailAddress);
    Task<bool> IsInRoleAsync(ApplicationUser user, string roleName);

    Task<DeleteResponseDto> DeleteUserAsync(string userId);

    Task<UserDetailsDto?> UpdateUserAsync(UpdateUserDetailsDto dto);

    Task<List<UserDetailsDto>> GetAllUsers();


}