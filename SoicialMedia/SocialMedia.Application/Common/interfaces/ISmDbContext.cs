namespace SocialMedia.Application.Common.interfaces;

public interface ISmDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    // public DbSet<Domain.Entities.Event> Events { get; }
}