using MediatR;

namespace SocialMedia.Application.Role.Commands.CreateRoleCommand;

public record CreateRoleCommand(string RoleName) : IRequest<string>;