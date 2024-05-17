using Microsoft.EntityFrameworkCore;
using SocialMedia.Domain.Entities;
using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Application.Common.interfaces;

public interface ISmDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    public DbSet<Comment> Comments { get; }
    public DbSet<Event> Events { get; }
    public DbSet<Friendship> Friendships { get; }
    public DbSet<Like> Likes { get; }
    public DbSet<Message> Messages { get; }
    public DbSet<Post> Posts { get; }
    public DbSet<ApplicationUser> Users { get; }
}