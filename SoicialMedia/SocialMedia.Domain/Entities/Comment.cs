namespace SocialMedia.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    
    public string UserId { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
    
    public int LikeCount { get; set; }
}