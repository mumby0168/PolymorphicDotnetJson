using Newtonsoft.Json;

namespace PolymorphicDotnetJson;

public abstract class EventStream
{
    public virtual string GetEventType() =>
        GetType().Name;
    
    private DateTime EventCreatedUtc { get; init; }
    
    [JsonConverter(typeof(EventConverter))]
    public Event Payload { get; set; }
}