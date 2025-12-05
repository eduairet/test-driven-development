namespace RsvpApp.Core.Models;

public class EventBookingRequest
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public string EventName { get; set; }
    public DateTime EventTime { get; set; }
    public EventType EventType { get; set; }
}