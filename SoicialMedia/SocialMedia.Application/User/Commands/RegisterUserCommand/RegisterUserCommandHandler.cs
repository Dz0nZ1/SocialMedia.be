using MediatR;
using SocialMedia.Application.Common.Constants;
using SocialMedia.Application.Common.Dto.User;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Application.Common.Mappers.User;

namespace SocialMedia.Application.User.Commands.RegisterUserCommand;

public class RegisterUserCommandHandler(IUserService userService) : IRequestHandler<RegisterUserCommand, UserDetailsDto?>
{
    public async Task<UserDetailsDto?> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
      return await userService.CreateUserAsync(request.User,
           [AuthorizationConstants.User]);
    }
}