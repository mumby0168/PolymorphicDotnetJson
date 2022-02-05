using System.Text.Json.Serialization;
using Newtonsoft.Json;
using PolymorphicDotnetJson.SystemText;

namespace PolymorphicDotnetJson;

public abstract class Event
{

    public Event() => 
        EventName = GetType().Name;

    [JsonProperty(Order = -5, PropertyName = "eventName")]
    [JsonPropertyOrder(0)]
    [JsonPropertyName("__eventName")]
    protected string EventName { get; set; }
}