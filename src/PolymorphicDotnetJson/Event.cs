using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace PolymorphicDotnetJson;

public abstract class Event
{

    public Event() => 
        EventName = GetType().Name;

    [JsonProperty(Order = -5, PropertyName = "eventName")]
    [JsonPropertyOrder(-5)]
    protected string EventName { get; set; }
}