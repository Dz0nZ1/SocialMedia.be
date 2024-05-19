using Ardalis.GuardClauses;
using MediatR;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.User;
using NotFoundException = SocialMedia.Application.Common.Exceptions.NotFoundException;

namespace SocialMedia.Application.User.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler(IUserService userService) : IRequestHandler<UpdateUserCommand, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        return await userService.UpdateUserAsync(request.User);
    }
}