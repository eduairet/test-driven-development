using RsvpApp.Core.Models;

namespace RsvpApp.Core.Domain;

public class EventBooking
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public string FullName { get; set; }
    public string Email { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; }
    public EventType Type { get; set; }
}
