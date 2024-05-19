using EMS.Application.Common.Dto.Auth;
using MediatR;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Auth.Commands.BeginLoginCommand;

public class BeginLoginCommandHandler(IAuthService authService) : IRequestHandler<SocialMedia.Application.Auth.Commands.BeginLoginCommand.BeginLoginCommand, BeginLoginResponseDto>
{
    public async Task<BeginLoginResponseDto> Handle(SocialMedia.Application.Auth.Commands.BeginLoginCommand.BeginLoginCommand request, CancellationToken cancellationToken)
    {
       return await authService.BeginLoginAsync(request.EmailAddress);
    }
}