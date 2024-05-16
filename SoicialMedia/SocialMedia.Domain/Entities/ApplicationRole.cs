using Microsoft.AspNetCore.Identity;

namespace SocialMedia.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    #region Properties

    public IList<ApplicationUserRole> UserRoles { get; set; }

    #endregion
}