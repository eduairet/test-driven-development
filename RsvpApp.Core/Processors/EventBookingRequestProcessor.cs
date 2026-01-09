using RsvpApp.Core.Domain;
using RsvpApp.Core.Enums;
using RsvpApp.Core.Models;
using RsvpApp.Core.Services;

namespace RsvpApp.Core.Processors;

public class EventBookingRequestProcessor(IEventBookingService eventBookingService, List<Event> events)
{
    private readonly IEventBookingService eventBookingService = eventBookingService;
    private readonly List<Event> _events = events;

    public EventBookingResult BookEvent(EventBookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        var result = MapRequestToResult<EventBookingResult>(request);
        result.Id = Guid.NewGuid();

        var evt = _events.FirstOrDefault(e => e.Id == request.EventId);
        if (eventBookingService.GetEventAvailability(request.EventId))
        {
            eventBookingService.Save(result);
            result.SuccessFlag = BookingSuccessFlag.Success;
            evt.Count++;
        }
        else
        {
            result.SuccessFlag = BookingSuccessFlag.Failed;
        }

        return result;
    }

    private static TEventBooking MapRequestToResult<TEventBooking>(EventBookingRequest request) where TEventBooking : EventBookingBase, new()
    {
        return new TEventBooking
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            EventId = request.EventId
        };
    }
}
