using MediatR;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.User.Commands.CreateUserCommand;

public class CreateUserCommandHandler(IUserService userService) : IRequestHandler<SocialMedia.Application.User.Commands.CreateUserCommand.CreateUserCommand, string>
{
    public async Task<string> Handle(SocialMedia.Application.User.Commands.CreateUserCommand.CreateUserCommand request, CancellationToken cancellationToken)
    {
        await userService.CreateUserAsync(request.EmailAddress, request.Roles);
        return "User was successfully created";
    }
}