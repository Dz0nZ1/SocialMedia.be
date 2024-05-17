using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Domain.Entities.User;

public class ApplicationUser : IdentityUser
{
   #region Properties

   public string FirstName { get; set; }

   public string LastName { get; set; }

   public string Username { get; set; }
   
   public string ProfilePictureUrl { get; set; }

   public string Bio { get; set; }

   public DateTime CreatedAt { get; set; }
   
   public DateTime ModifiedAt { get; set; }
   
   public IList<ApplicationUserRole> Roles { get; } = new List<ApplicationUserRole>();

   public List<Post> Posts = new List<Post>();

   public List<Like> Likes = new List<Like>();

   public List<Comment> Comments = new List<Comment>();

   #endregion

}