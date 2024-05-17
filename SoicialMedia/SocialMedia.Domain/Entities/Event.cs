using System.Runtime.InteropServices.JavaScript;

namespace SocialMedia.Domain.Entities;

public class Event
{
    public Guid EventId { get; set; }
    
    public string OrganizerId { get; set; }
    
    public string Title { get; set; }

    public string Description { get; set; }
    
    public string Location { get; set; }
    
    public DateTime StartTime { get; set; }
    
    public DateTime EndTime { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public DateTime ModifiedAt { get; set; }
}