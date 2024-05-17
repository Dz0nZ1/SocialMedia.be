using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SocialMedia.Domain.Entities;

namespace SocialMedia.Infrastructure.Domain.Post;

public class LikesConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder.ToTable("Likes");
        builder.HasKey(x => x.Id);

        //Relationship with Post
        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.PostId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        //Relationship with Comment
        builder
            .HasOne(x => x.Comment)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.CommentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
        //Relationship with User
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}