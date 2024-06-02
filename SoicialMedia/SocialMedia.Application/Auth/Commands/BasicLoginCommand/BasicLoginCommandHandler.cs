using MediatR;
using SocialMedia.Application.Common.Dto.Auth;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Auth.Commands.BasicLoginCommand;

public class BasicLoginCommandHandler(IAuthService authService) : IRequestHandler<BasicLoginCommand, CompleteLoginResponseDto>
{
    public async Task<CompleteLoginResponseDto> Handle(BasicLoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.BasicLoginAsync(request.User);
    }
}