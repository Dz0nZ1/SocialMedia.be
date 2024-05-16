using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Domain.Identity;

public class ApplicationRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
{
    private const string AdminId = "40FEB7B4-B530-4EA2-B96F-582D88277E4B";
    private const string UserId = "34DE6D7C-4270-425B-987F-8D2CC41CD857";
    
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {
        builder.ToTable("Roles");

        var adminRole = new ApplicationRole
        {
            Id = AdminId,
            Name = "Administrator",
            NormalizedName = "ADMINISTRATOR",
            ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8036"
        };

        var studentServiceRole = new ApplicationRole
        {
            Id = UserId,
            Name = "User",
            NormalizedName = "USER",
            ConcurrencyStamp = "a09ab67f-02d6-4910-8659-3385759d8037"
        };
        
        builder.HasData(adminRole);
        builder.HasData(studentServiceRole);
        
    }
}