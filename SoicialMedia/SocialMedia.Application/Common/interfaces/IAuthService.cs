using EMS.Application.Common.Dto.Auth;

namespace SocialMedia.Application.Common.interfaces;

public interface IAuthService
{
    Task<BeginLoginResponseDto> BeginLoginAsync(string emailAddress);
    Task<CompleteLoginResponseDto> CompleteLoginAsync(string token);
}