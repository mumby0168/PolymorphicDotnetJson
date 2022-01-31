using Newtonsoft.Json;

namespace PolymorphicDotnetJson;

public abstract class Event
{

    public Event() => 
        EventName = GetType().Name;

    [JsonProperty(Order = -5, PropertyName = "eventName")]
    protected string EventName { get; set; }
}