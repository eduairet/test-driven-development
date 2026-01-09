using RsvpApp.Core.Domain;
using RsvpApp.Core.Models;

namespace RsvpApp.Core.Services;

public interface IEventBookingService
{
    void Save(EventBookingResult eventBooking);
    bool GetEventAvailability(Guid eventId);
}
