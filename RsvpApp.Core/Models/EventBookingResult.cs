using RsvpApp.Core.Domain;
using RsvpApp.Core.Enums;

namespace RsvpApp.Core.Models;

public class EventBookingResult : EventBookingBase
{
    public Guid Id { get; set; }
    public BookingSuccessFlag SuccessFlag { get; set; }
}
