using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Infrastructure.Domain.Identity;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    private const string AdminId = "4DAF65CB-CC0E-4C81-9183-20097EA81F5A";
    
    
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.ToTable("Users");
        
        
        var admin = new ApplicationUser
        {
            Id = AdminId,
            FirstName = "Nikola",
            LastName = "Lelekovic",
            Username = "dzonzi",
            ProfilePictureUrl = "www.profile-picture.com/my-profile-picture.jpeg",
            Bio = "Software Engineer working full time",
            CreatedAt = DateTime.Now,
            ModifiedAt = DateTime.Now,
            PhoneNumber = "123-456-789",
            JobPosition = "Software Engineer",
            Location = "Belgrade, Serbia",
            UserName = "nikola@email.com",
            NormalizedUserName = "NIKOLA@EMAIL.COM",
            Email = "nikola@email.com",
            NormalizedEmail = "NIKOLA@EMAIL.COM",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = new Guid().ToString("D"),
            ConcurrencyStamp = "c188a435-cfc8-45fd-836c-9a18bb9de405",
            AccessFailedCount = 0
        };

        builder.HasData(admin);

        //Relationship with roles
        builder
            .HasMany(x => x.Roles)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        //Relationship with posts
        builder
            .HasMany(x => x.Posts)
            .WithOne()
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}