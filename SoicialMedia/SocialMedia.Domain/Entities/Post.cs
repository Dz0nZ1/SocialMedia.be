using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Post
{
    public Guid Id { get; private set; }
    
    public string UserId { get;  private set; }
    
    public ApplicationUser User { get;  private set; }
    
    public string Content { get;  private set; }
    
    public string ImageUrl { get;  private set; }
    
    public DateTime CreatedAt { get;  private set; }
    
    public DateTime ModifiedAt { get; private set; }
    
    public int LikeCount { get; private set; }
    
    public int CommentCount { get; private set; }
}