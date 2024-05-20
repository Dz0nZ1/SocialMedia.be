namespace SocialMedia.Domain.Entities.User;

public class RefreshToken
{
    public Guid Id { get; set; }
    public string UserId { get; set; }

    public ApplicationUser User;
    public string Token { get; set; }
    public DateTime Expiration { get; set; }
    public bool IsRevoked { get; set; }
}