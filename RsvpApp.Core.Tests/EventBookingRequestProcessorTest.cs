using RsvpApp.Core.Models;
using RsvpApp.Core.Processors;

namespace RsvpApp.Core;

public class EventBookingRequestProcessorTest
{
    // Some people use pascal case "ShouldReturnEventBookingRequest" and some use underscores "Should_Return_Event_Booking_Request" for test method names.
    [Fact]
    public void ShouldReturnEventBookingRequest()
    {
        // Arrange
        EventBookingRequest eventBookingRequest = new()
        {
            FullName = "Test User",
            Email = "test@test.com",
            EventName = "Test Event",
            EventType = EventType.Conference,
            EventTime = new DateTime(2024, 12, 25, 10, 0, 0)
        };

        EventBookingRequestProcessor processor = new();

        // Act
        EventBookingResult result = EventBookingRequestProcessor.BookEvent(eventBookingRequest);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Id.ToString()));
        Assert.Equal(eventBookingRequest.FullName, result.FullName);
        Assert.Equal(eventBookingRequest.Email, result.Email);
        Assert.Equal(eventBookingRequest.EventName, result.EventName);
        Assert.Equal(eventBookingRequest.EventTime, result.EventTime);
        Assert.Equal(eventBookingRequest.EventType, result.EventType);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenRequestIsNull()
    {
        // Arrange
        EventBookingRequest eventBookingRequest = null;
        EventBookingRequestProcessor processor = new();
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => EventBookingRequestProcessor.BookEvent(eventBookingRequest));
    }
}
