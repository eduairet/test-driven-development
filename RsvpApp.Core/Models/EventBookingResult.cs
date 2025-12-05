namespace RsvpApp.Core.Models;

public class EventBookingResult
{
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    public string FullName { get; set; }
    public string Email { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; }
    public EventType EventType { get; set; }
}
