using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Domain.Entities.User;

public class ApplicationUserRole : IdentityUserRole<string>
{
    #region Properties

    public ApplicationUser User { get; set; }
    
    public ApplicationRole Role { get; set; }

    #endregion
}