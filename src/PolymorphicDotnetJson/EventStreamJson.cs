using Newtonsoft.Json;

namespace PolymorphicDotnetJson;

public class EventStreamJson
{
    [JsonProperty(Order = -10, PropertyName = "eventType")]
    public string EventType { get; init; }
    
    public EventStreamDtoBased StreamDtoBased { get; set; }
}