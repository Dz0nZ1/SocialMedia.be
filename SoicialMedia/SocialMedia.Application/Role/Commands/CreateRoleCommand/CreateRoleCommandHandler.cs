using MediatR;
using SocialMedia.Application.Common.interfaces;

namespace SocialMedia.Application.Role.Commands.CreateRoleCommand;

public class CreateRoleCommandHandler(IRoleService roleService) : IRequestHandler<SocialMedia.Application.Role.Commands.CreateRoleCommand.CreateRoleCommand, string>
{
    public async Task<string> Handle(SocialMedia.Application.Role.Commands.CreateRoleCommand.CreateRoleCommand request, CancellationToken cancellationToken)
    {
        await roleService.CreateRoleAsync(request.RoleName);
        return "Role was successfully created";
    }
}