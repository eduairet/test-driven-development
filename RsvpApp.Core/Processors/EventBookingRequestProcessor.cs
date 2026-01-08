using RsvpApp.Core.Domain;
using RsvpApp.Core.Models;
using RsvpApp.Core.Services;

namespace RsvpApp.Core.Processors;

public class EventBookingRequestProcessor
{
    private IEventBookingService @object;

    public EventBookingRequestProcessor(IEventBookingService @object)
    {
        this.@object = @object;
    }

    public EventBooking BookEvent(EventBookingRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        return new EventBooking
        {
            Id = Guid.NewGuid(),
            CreatedDate = DateTime.Now,
            UpdatedDate = DateTime.Now,
            FullName = request.FullName,
            Email = request.Email,
            EventName = request.EventName,
            EventTime = request.EventTime,
            Type = request.EventType,
        };
    }
}
