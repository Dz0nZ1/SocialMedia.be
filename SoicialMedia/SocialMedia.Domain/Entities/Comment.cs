using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }
    
    public Guid PostId { get; set; }
    
    public Post Post { get; set; }
    
    public string? UserId { get; set; }
    
    public ApplicationUser? User { get; set; }
    
    public string Content { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }

    public List<Like> Likes = new List<Like>();
}