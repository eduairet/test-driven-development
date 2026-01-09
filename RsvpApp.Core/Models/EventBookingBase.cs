namespace RsvpApp.Core.Models;

public class EventBookingBase
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public Guid EventId { get; set; }
}