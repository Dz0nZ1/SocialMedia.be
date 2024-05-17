using SocialMedia.Domain.Entities.User;

namespace SocialMedia.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string SenderId { get; set; }
    public string ReceiverId { get; set; }
    public string Content { get; set; }
    public string CreatedAt { get; set; }
    public string? ReadAt { get; set; } 
}