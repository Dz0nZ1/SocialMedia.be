using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialMedia.Application.Common.interfaces;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Infrastructure.Contexts;

public class SmDbContext(DbContextOptions<SmDbContext> options): IdentityDbContext<ApplicationUser,
    ApplicationRole,
    string,
    IdentityUserClaim<string>,
    ApplicationUserRole,
    IdentityUserLogin<string>,
    IdentityRoleClaim<string>,
    IdentityUserToken<string>
>(options), ISmDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                {
                    property.SetValueConverter(new ValueConverter<DateTime, DateTime>(
                        v => v.ToUniversalTime(),
                        v => DateTime.SpecifyKind(v, DateTimeKind.Utc)));
                }
            }
        }
        
        
    }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     // if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") != "Test")
    //     // {
    //     //     optionsBuilder.UseNpgsql("Host=localhost;Username=postgres;Password=root;Database=SocialMedia");
    //     // }
    // }
    
    // public DbSet<Event> Events => Set<Event>();
    public override DbSet<ApplicationUser> Users => Set<ApplicationUser>();
  

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}