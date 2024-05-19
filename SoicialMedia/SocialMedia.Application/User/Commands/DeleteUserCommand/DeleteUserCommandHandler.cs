using System.Net;
using Ardalis.GuardClauses;
using MediatR;
using SocialMedia.Application.Common.Dto.Common;
using SocialMedia.Application.Common.interfaces;
using NotFoundException = SocialMedia.Application.Common.Exceptions.NotFoundException;

namespace SocialMedia.Application.User.Commands.DeleteUserCommand;

public class DeleteUserCommandHandler(IUserService userService) : IRequestHandler<DeleteUserCommand, DeleteResponseDto>
{
    public async Task<DeleteResponseDto> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        return await userService.DeleteUserAsync(request.Id);
    }
}