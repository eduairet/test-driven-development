using RsvpApp.Core.Domain;

namespace RsvpApp.Core.Services;

public interface IEventBookingService
{
    void Save(EventBooking eventBooking);
}
