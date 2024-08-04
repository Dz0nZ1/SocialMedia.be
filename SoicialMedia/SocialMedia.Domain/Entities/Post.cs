using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Post
{
 #region Properties

 public Guid Id { get; private set; }
    
 public string UserId { get;  private set; }
    
 public ApplicationUser User { get;  private set; }
    
 public string Content { get;  private set; }
    
 public string ImageUrl { get;  private set; }
    
 public DateTime CreatedAt { get;  private set; }
    
 public DateTime ModifiedAt { get; private set; }
    
 public List<Like> Likes = new List<Like>();

 public List<Comment> Comments = new List<Comment>();

 #endregion
 
 #region Extensions

 public Post(string content, string imageUrl)
 {
     Id = Guid.NewGuid();
     Content = content;
     ImageUrl = imageUrl;
 }

 public Post AddCreatedAt(DateTime createdAt)
 {
     CreatedAt = createdAt;
     return this;
 }

 public Post AddModifiedAt(DateTime modifiedAt)
 {
     ModifiedAt = modifiedAt;
     return this;
 }

 public Post AddUser(ApplicationUser user)
 {
     User = user;
     return this;
 }

 public Post UpdatePost(string content)
 {
     Content = content;
     ModifiedAt = DateTime.Now;
     return this;
 }

 #endregion
 
}