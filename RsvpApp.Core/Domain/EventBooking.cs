using RsvpApp.Core.Models;

namespace RsvpApp.Core.Domain;

public class EventBooking : EventBookingBase
{
    public Guid Id { get; set; }
}
