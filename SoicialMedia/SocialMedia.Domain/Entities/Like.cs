namespace SocialMedia.Domain.Entities;

public class Like
{
    public Guid Id { get; set; }

    public string UserId { get; set; }

    public Guid? PostId { get; set; }

    public Guid? CommentId { get; set; }
    
    public DateTime CreatedAt { get; set; }
}