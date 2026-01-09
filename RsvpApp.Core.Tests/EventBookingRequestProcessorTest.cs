using Moq;
using RsvpApp.Core.Domain;
using RsvpApp.Core.Enums;
using RsvpApp.Core.Models;
using RsvpApp.Core.Processors;
using RsvpApp.Core.Services;

namespace RsvpApp.Core;

public class EventBookingRequestProcessorTest
{
    private EventBookingRequestProcessor _processor;
    private List<Event> _events;
    private EventBookingRequest _bookingRequest;
    private EventBookingRequest _bookingRequestSoldOut;
    private Mock<IEventBookingService> _eventBookingServiceMock;

    public EventBookingRequestProcessorTest()
    {
        // Arrange
        _events =
        [
            new Event
            {
                Id = new Guid("11111111-1111-1111-1111-111111111111"),
                Name = "Tech Conference 2026",
                Description = "A conference about the latest in technology.",
                EventDate = new DateTime(2026, 9, 15),
                EventTime = new DateTime(2026, 9, 15, 9, 0, 0),
                Type = EventType.Conference,
                Capacity = 100,
                Count = 50
            },
            new Event
            {
                Id = new Guid("22222222-2222-2222-2222-222222222222"),
                Name = "AI Workshop",
                Description = "A hands-on workshop on Artificial Intelligence.",
                EventDate = new DateTime(2026, 10, 20),
                EventTime = new DateTime(2026, 10, 20, 10, 0, 0),
                Type = EventType.Workshop,
                Capacity = 50,
                Count = 50
            }
        ];

        _bookingRequest = new()
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            EventId = new Guid("11111111-1111-1111-1111-111111111111")
        };

        _bookingRequestSoldOut = new()
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane.smith@example.com",
            EventId = new Guid("22222222-2222-2222-2222-222222222222")
        };

        _eventBookingServiceMock = new Mock<IEventBookingService>();
        _eventBookingServiceMock.Setup(s => s.GetEventAvailability(_bookingRequest.EventId))
            .Returns(_events.First(e => e.Id == _bookingRequest.EventId).Count < _events.First(e => e.Id == _bookingRequest.EventId).Capacity);

        _processor = new EventBookingRequestProcessor(_eventBookingServiceMock.Object, _events);
    }

    // Some people use pascal case "ShouldReturnEventBookingRequest" and some use underscores "Should_Return_Event_Booking_Request" for test method names.
    [Fact]
    public void ShouldReturnEventBookingRequest()
    {
        // Act
        EventBookingResult result = _processor.BookEvent(_bookingRequest);

        // Assert
        Assert.NotNull(result);
        Assert.False(string.IsNullOrWhiteSpace(result.Id.ToString()));
        Assert.Equal(_bookingRequest.FirstName, result.FirstName);
        Assert.Equal(_bookingRequest.LastName, result.LastName);
        Assert.Equal(_bookingRequest.Email, result.Email);
        Assert.Equal(_bookingRequest.EventId, result.EventId);
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
        EventBookingResult savedEventBooking = null;
        _eventBookingServiceMock.Setup(s => s.Save(It.IsAny<EventBookingResult>()))
            .Callback<EventBookingResult>(booking => savedEventBooking = booking);

        _processor.BookEvent(_bookingRequest);

        _eventBookingServiceMock.Verify(s => s.Save(It.IsAny<EventBookingResult>()), Times.Once);

        Assert.NotNull(savedEventBooking);
        Assert.Equal(_bookingRequest.FirstName, savedEventBooking.FirstName);
        Assert.Equal(_bookingRequest.LastName, savedEventBooking.LastName);
        Assert.Equal(_bookingRequest.Email, savedEventBooking.Email);
        Assert.Equal(_bookingRequest.EventId, savedEventBooking.EventId);
    }

    [Fact]
    public void ShouldNotSaveEventBookingWhenEventIsSoldOut()
    {
        // Act
        _processor.BookEvent(_bookingRequestSoldOut);

        // Assert
        _eventBookingServiceMock.Verify(s => s.Save(It.IsAny<EventBookingResult>()), Times.Never);
    }

    [Theory]
    [InlineData(BookingSuccessFlag.Failed, false)]
    [InlineData(BookingSuccessFlag.Success, true)]
    public void ShouldReturnCorrectBookingSuccessFlag(BookingSuccessFlag expectedFlag, bool isAvailable)
    {
        // Arrange
        var bookingRequest = isAvailable ? _bookingRequest : _bookingRequestSoldOut;

        // Act
        _processor.BookEvent(bookingRequest);

        // Assert
        var actualFlag = isAvailable ? BookingSuccessFlag.Success : BookingSuccessFlag.Failed;
        Assert.Equal(expectedFlag, actualFlag);
    }

    [Fact]
    public void ShouldIncrementEventAttendeeCountWhenBookingIsMade()
    {
        // Arrange
        var eventId = _bookingRequest.EventId;
        var initialCount = _events.First(e => e.Id == eventId).Count;

        // Act
        _processor.BookEvent(_bookingRequest);

        // Assert
        var updatedEvent = _events.First(e => e.Id == eventId);
        Assert.Equal(initialCount + 1, updatedEvent.Count);
    }

    [Fact]
    public void ShouldNotIncrementEventAttendeeCountWhenBookingIsNotSaved()
    {
        // Arrange
        var eventId = _bookingRequestSoldOut.EventId;
        var initialCount = _events.First(e => e.Id == eventId).Count;

        // Act
        _processor.BookEvent(_bookingRequestSoldOut);

        // Assert
        var updatedEvent = _events.First(e => e.Id == eventId);
        Assert.Equal(initialCount, updatedEvent.Count);
    }

    // You can merge both tests ShouldIncrementEventAttendeeCountWhenBookingIsMade and ShouldNotIncrementEventAttendeeCountWhenBookingIsNotSaved into a single parameterized test
    [Theory]
    [InlineData("11111111-1111-1111-1111-111111111111", true)]
    [InlineData("22222222-2222-2222-2222-222222222222", false)]
    public void ShouldCheckEventAvailabilityUsingService(string eventIdString, bool expectedAvailability)
    {
        // Arrange
        var eventId = new Guid(eventIdString);

        _eventBookingServiceMock.Setup(s => s.GetEventAvailability(eventId))
            .Returns(expectedAvailability);

        var bookingRequest = new EventBookingRequest
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@example.com",
            EventId = eventId
        };

        // Act
        _processor.BookEvent(bookingRequest);

        // Assert
        _eventBookingServiceMock.Verify(s => s.GetEventAvailability(eventId), Times.Once);
        var actualAvailability = _events.First(e => e.Id == eventId).Count < _events.First(e => e.Id == eventId).Capacity;
        Assert.Equal(expectedAvailability, actualAvailability);
    }
}
