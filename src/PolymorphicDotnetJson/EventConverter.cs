using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PolymorphicDotnetJson;

public class EventConverter : JsonConverter<Event>
{
    public static List<Type> ConvertableTypes { get; } = new()
    {
        typeof(ItemAddedToCart)
    };

    public override void WriteJson(JsonWriter writer, Event? value, JsonSerializer serializer) =>
        serializer.Serialize(writer, value, value?.GetType());

    public override Event? ReadJson(
        JsonReader reader, 
        Type objectType, 
        Event? existingValue, 
        bool hasExistingValue,
        JsonSerializer serializer)
    {
        var j = JToken.ReadFrom(reader);
        var type = j["eventName"]?.ToString();
        var payloadType = ConvertableTypes.First(x => x.Name == type);
        return (Event?) j.ToObject(payloadType);
    }
}