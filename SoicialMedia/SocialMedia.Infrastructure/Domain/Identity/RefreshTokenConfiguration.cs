using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Infrastructure.Domain.Identity;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshToken");
        builder.HasKey(x => x.Id);
        
        //Relationship with User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.RefreshTokens)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Token settings
        builder.Property(x => x.Token)
            .IsRequired()
            .HasMaxLength(200);
        
        builder.HasIndex(x => x.Token).IsUnique();
        builder.HasIndex(x => x.UserId);

        builder.Property(x => x.Expiration)
            .IsRequired();
        
        builder.Property(x => x.IsRevoked)
            .IsRequired();
    }
}