using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Domain.Entities.User;

public class ApplicationRole : IdentityRole
{
    #region Properties

    public IList<ApplicationUserRole> UserRoles { get; set; }

    #endregion
}