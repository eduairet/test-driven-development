using RsvpApp.Core.Enums;

namespace RsvpApp.Core.Domain;

public class Event
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime EventDate { get; set; }
    public DateTime EventTime { get; set; }
    public EventType Type { get; set; }
    public int Capacity { get; set; }
    public int Count { get; set; }
}

