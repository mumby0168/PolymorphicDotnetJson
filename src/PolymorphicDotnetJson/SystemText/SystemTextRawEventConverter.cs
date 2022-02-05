using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDotnetJson.SystemText;

public class SystemTextRawEventConverter : JsonConverter<Event>
{
    public static List<Type> ConvertableTypes { get; } = new()
    {
        typeof(ItemAddedToCart)
    };
    
    public override Event? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        if (reader.GetString() != "EventName")
        {
            throw new JsonException();
        }
        
        reader.Read();
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var str = reader.GetString();
        
        var type = ConvertableTypes.First(x => x.Name == str);
        return (Event?)JsonSerializer.Deserialize(ref reader, type, options);
    }

    public override void Write(Utf8JsonWriter writer, Event value, JsonSerializerOptions options) => 
        JsonSerializer.Serialize(value, options);
}