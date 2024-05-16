using MediatR;

namespace SocialMedia.Application.User.Commands.CreateUserCommand;

public record CreateUserCommand(string EmailAddress, List<string> Roles) : IRequest<string>;