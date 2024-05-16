namespace SocialMedia.Application.Common.interfaces;

public interface ICurrentUserService
{
    public string? UserId { get;  set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public List<string>? Roles { get; set; }
    bool IsAdministrator { get; }
}