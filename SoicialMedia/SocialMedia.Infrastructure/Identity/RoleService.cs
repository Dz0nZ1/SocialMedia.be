using Microsoft.AspNetCore.Identity;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Identity;

public class RoleService(RoleManager<ApplicationRole> roleManager) : IRoleService
{
    public async Task CreateRoleAsync(string role)
    {
        var roleAlreadyExists = await roleManager.RoleExistsAsync(role);

        if (!roleAlreadyExists)
        {
            await roleManager.CreateAsync(new ApplicationRole()
            {
                Name = role,
                NormalizedName = role.Normalize()
            });
        }
        
    }
}