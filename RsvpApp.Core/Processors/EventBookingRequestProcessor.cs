using RsvpApp.Core.Models;

namespace RsvpApp.Core.Processors;

public class EventBookingRequestProcessor
{
    public static EventBookingResult BookEvent(EventBookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new EventBookingResult
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            FullName = request.FullName,
            Email = request.Email,
            EventName = request.EventName,
            EventTime = request.EventTime,
            EventType = request.EventType,
        };
    }
}
