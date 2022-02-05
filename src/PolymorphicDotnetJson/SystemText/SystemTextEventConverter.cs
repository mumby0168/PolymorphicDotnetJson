using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDotnetJson.SystemText;

public class SystemTextEventConverter : JsonConverter<Event>
{
    public static List<Type> ConvertableTypes { get; } = new()
    {
        typeof(ItemAddedToCart)
    };
    
    public override Event? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (!JsonDocument.TryParseValue(ref reader, out var doc))
        {
            return null;
        }
        
        if (doc.RootElement.TryGetProperty("EventName", out var element))
        {
            var val = element.GetString();
            if (val is null)
                return null;

            var type = ConvertableTypes.First(x => x.Name == val);
            return (Event?)JsonSerializer.Deserialize(ref reader, type, options);
        }

        return null;

    }

    public override void Write(Utf8JsonWriter writer, Event value, JsonSerializerOptions options) => 
        JsonSerializer.Serialize(value, options);
}