using MediatR;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Auth.Commands.LogoutCommand;

public class LogoutCommandHandler(IAuthService authService) : IRequestHandler<LogoutCommand>
{
    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        await authService.LogoutAsync(request.UserId);
    }
}