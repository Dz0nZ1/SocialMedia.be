using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Domain.Entities.User;
using SocialMedia.Infrastructure.Configuration;
using SocialMedia.Infrastructure.Contexts;
using SocialMedia.Infrastructure.Identity;
using SocialMedia.Infrastructure.Services;

namespace SocialMedia.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {

        //Database
        var dbConfiguration = new PostgresDbConfiguration();
        configuration.GetSection("PostgresDbConfiguration").Bind(dbConfiguration);
        
        serviceCollection.AddDbContext<SmDbContext>(
            options => options.UseNpgsql(dbConfiguration.ConnectionString,
                x => x.MigrationsAssembly(typeof(SmDbContext).Assembly.FullName))
        );
        serviceCollection.AddScoped<ISmDbContext>(provider => provider.GetRequiredService<SmDbContext>());
        
        //Business logic
        
        //Authentication
        serviceCollection
            .AddIdentity<ApplicationUser, ApplicationRole>()
            .AddRoleManager<RoleManager<ApplicationRole>>()
            .AddUserManager<ApplicationUserManager>()
            .AddEntityFrameworkStores<SmDbContext>()
            .AddDefaultTokenProviders();
        serviceCollection.AddScoped<IAuthService, AuthService>();
        serviceCollection.AddScoped<IUserService, UserService>();
        serviceCollection.AddScoped<IRoleService, RoleService>();
        serviceCollection.AddScoped<ICurrentUserService, CurrentUserService>();
        serviceCollection.Configure<JwtConfiguration>(configuration.GetSection("JwtConfiguration"));
        
        
        return serviceCollection;
    }
}