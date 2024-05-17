using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SocialMedia.Infrastructure.Domain.Post;

public class PostConfiguration : IEntityTypeConfiguration<SocialMedia.Domain.Entities.Post>
{
    public void Configure(EntityTypeBuilder<SocialMedia.Domain.Entities.Post> builder)
    {
        builder.ToTable("Posts");
        builder.HasKey(x => x.Id);

        //Relation with User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}