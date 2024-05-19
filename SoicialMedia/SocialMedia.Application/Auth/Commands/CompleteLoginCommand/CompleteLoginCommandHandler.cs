using EMS.Application.Common.Dto.Auth;
using MediatR;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Auth.Commands.CompleteLoginCommand;

public class CompleteLoginCommandHandler(IAuthService authService) : IRequestHandler<SocialMedia.Application.Auth.Commands.CompleteLoginCommand.CompleteLoginCommand, CompleteLoginResponseDto>
{
    public async Task<CompleteLoginResponseDto> Handle(SocialMedia.Application.Auth.Commands.CompleteLoginCommand.CompleteLoginCommand request, CancellationToken cancellationToken)
    {
        return await authService.CompleteLoginAsync(request.ValidationToken);
    }
}