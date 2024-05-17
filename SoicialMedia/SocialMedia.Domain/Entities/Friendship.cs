namespace SocialMedia.Domain.Entities;

public class Friendship
{
    public Guid Id { get; set; }
    
    public string FirstUserId { get; set; }
    
    public string SeconedUserId { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public string StatusEnum { get; set; }
}