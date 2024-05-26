using MediatR;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<SocialMedia.Application.User.Commands.CreateUserCommand.CreateUserCommand, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> Handle(SocialMedia.Application.User.Commands.CreateUserCommand.CreateUserCommand request, CancellationToken cancellationToken)
    {
        return  await userService.CreateUserAsync(request.User, request.Roles);
    }
}