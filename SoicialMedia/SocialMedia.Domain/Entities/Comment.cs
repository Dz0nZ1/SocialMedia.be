using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Comment
{
    #region Properties

    public Guid Id { get; private set; }
    
    public Guid PostId { get; private set; }
    
    public Post Post { get; private set; }
    
    public string? UserId { get; private set; }
    
    public ApplicationUser? User { get; private set; }
    
    public string Content { get; private set; }
    
    public DateTime CreatedAt { get; private set; }
    
    public DateTime ModifiedAt { get; private set; }

    public List<Like> Likes = new List<Like>();

    #endregion

    #region Constructors

    public Comment(string content, string userId, Guid postId)
    {
        Content = content;
        UserId = userId;
        PostId = postId;
        Id = Guid.NewGuid();
       
    }
    
    public Comment(string content)
    {
        Content = content;
    }

    #endregion
    

    #region Extensions

    public Comment AddUser(ApplicationUser user)
    {
        User = user;
        return this;
    }

    public Comment AddPost(Post post)
    {
        Post = post;
        return this;
    }

    public Comment AddCreatedAt(DateTime time)
    {
        CreatedAt = time;
        return this;
    }

    public Comment AddModifiedAt(DateTime time)
    {
        ModifiedAt = time;
        return this;
    }

    public Comment UpdateComment(string content)
    {
        Content = content;
        ModifiedAt = DateTime.Now;
        return this;
    }

    #endregion
}