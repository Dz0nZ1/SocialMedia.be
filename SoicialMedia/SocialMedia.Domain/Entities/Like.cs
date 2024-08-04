using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Like
{
    public Guid Id { get; private set; }

    public string UserId { get; private set; }
    
    public ApplicationUser User { get; private set; }

    public Guid? PostId { get; private set; }
    
    public Post? Post { get; private set; }

    public Guid? CommentId { get; private set; }

    public bool IsLocked { get; set; } = false;
    public Comment? Comment { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
}