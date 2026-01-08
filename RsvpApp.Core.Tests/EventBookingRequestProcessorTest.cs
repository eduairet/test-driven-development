using Moq;
using RsvpApp.Core.Domain;
using RsvpApp.Core.Models;
using RsvpApp.Core.Processors;
using RsvpApp.Core.Services;

namespace RsvpApp.Core;

public class EventBookingRequestProcessorTest
{
    private EventBookingRequestProcessor _processor;
    private EventBookingRequest _bookingRequest;
    private Mock<IEventBookingService> _eventBookingServiceMock;

    public EventBookingRequestProcessorTest()
    {
        // Arrange
        _bookingRequest = new()
        {
            FullName = "Test User",
            Email = "test@test.com",
            EventName = "Test Event",
            EventTime = new DateTime(2024, 12, 25, 10, 0, 0),
            EventType = EventType.Conference,
        };

        _eventBookingServiceMock = new Mock<IEventBookingService>();

        _processor = new EventBookingRequestProcessor(_eventBookingServiceMock.Object);
    }

    // Some people use pascal case "ShouldReturnEventBookingRequest" and some use underscores "Should_Return_Event_Booking_Request" for test method names.
    [Fact]
    public void ShouldReturnEventBookingRequest()
    {
        // Act
        EventBooking result = _processor.BookEvent(_bookingRequest);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Id.ToString()));
        Assert.Equal(_bookingRequest.FullName, result.FullName);
        Assert.Equal(_bookingRequest.Email, result.Email);
        Assert.Equal(_bookingRequest.EventName, result.EventName);
        Assert.Equal(_bookingRequest.EventTime, result.EventTime);
        Assert.Equal(_bookingRequest.EventType, result.Type);
    }

    [Fact]
    public void ShouldThrowArgumentNullExceptionWhenRequestIsNull()
    {
        // Arrange
        EventBookingRequest nullEventBookingRequest = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _processor.BookEvent(nullEventBookingRequest));
    }

    [Fact]
    public void ShouldSaveEventBookingUsingService()
    {
        EventBooking savedEventBooking = null;
        _eventBookingServiceMock.Setup(s => s.Save(It.IsAny<EventBooking>()))
            .Callback<EventBooking>(booking => savedEventBooking = booking);

        _processor.BookEvent(_bookingRequest);

        _eventBookingServiceMock.Verify(s => s.Save(It.IsAny<EventBooking>()), Times.Once);

        Assert.NotNull(savedEventBooking);
        Assert.Equal(_bookingRequest.FullName, savedEventBooking.FullName);
        Assert.Equal(_bookingRequest.Email, savedEventBooking.Email);
        Assert.Equal(_bookingRequest.EventName, savedEventBooking.EventName);
        Assert.Equal(_bookingRequest.EventTime, savedEventBooking.EventTime);
        Assert.Equal(_bookingRequest.EventType, savedEventBooking.Type);
    }
}
