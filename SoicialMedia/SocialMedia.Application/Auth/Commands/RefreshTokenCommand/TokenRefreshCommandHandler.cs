using MediatR;
using SocialMedia.Application.Common.Dto.Auth;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Auth.Commands.RefreshTokenCommand;

public class TokenRefreshCommandHandler(IAuthService authService) : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await authService.RefreshTokenAsync(request.Request);
    }
}