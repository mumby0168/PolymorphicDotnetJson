using Newtonsoft.Json;

namespace PolymorphicDotnetJson;

public abstract class EventStreamDtoBased
{
    public virtual string GetEventType() =>
        GetType().Name;
    
    private DateTime EventCreatedUtc { get; init; }
    
    public EventDto EventInstance { get; set; }
}

public abstract class EventStream
{
    public virtual string GetEventType() =>
        GetType().Name;
    
    private DateTime EventCreatedUtc { get; init; }
    
    public Event Payload { get; set; }
}