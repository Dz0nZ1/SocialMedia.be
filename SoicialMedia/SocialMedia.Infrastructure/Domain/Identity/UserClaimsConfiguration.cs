using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMedia.Infrastructure.Domain.Identity;

public class UserClaimsConfiguration : IEntityTypeConfiguration<IdentityUserClaim<string>>
{
    private const string AdminId = "4DAF65CB-CC0E-4C81-9183-20097EA81F5A";
    
    public void Configure(EntityTypeBuilder<IdentityUserClaim<string>> builder)
    {

        builder.ToTable("UserClaims");

        var adminIdClaim = new IdentityUserClaim<string>
        {   
            UserId = AdminId,
            Id = 1,
            ClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            ClaimValue = AdminId
        };

        var adminFirstName = new IdentityUserClaim<string>
        {
            UserId = AdminId,
            Id = 2,
            ClaimType = "FirstName",
            ClaimValue = "Nikola"
        };
        
        builder.HasData(adminIdClaim);
        builder.HasData(adminFirstName);

    }
}