using System.Text.Json;
using System.Text.Json.Serialization;

namespace PolymorphicDotnetJson.SystemText;

public class SystemTextEventDtoConverter : JsonConverter<EventDto>
{
    public static List<Type> ConvertableTypes { get; } = new()
    {
        typeof(ItemAddedToCart)
    };
    
    public override EventDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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

        var propName = reader.GetString();

        if (propName != "EventName")
        {
            throw new JsonException();
        }
        
        reader.Read();
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException();
        }

        var str = reader.GetString();
        
        reader.Read();
        if (reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new JsonException();
        }

        if (reader.GetString() != "Payload")
        {
            throw new JsonException();
        }

        reader.Read();
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException();
        }

        var type = ConvertableTypes.First(x => x.Name == str);
        var payload = JsonSerializer.Deserialize(ref reader, type, options);

        reader.Read();
        if (reader.TokenType != JsonTokenType.EndObject)
        {
            throw new JsonException();
        }
        
        return new EventDto()
        {
            EventName = str,
            Payload = payload
        };
    }

    public override void Write(Utf8JsonWriter writer, EventDto value, JsonSerializerOptions options) =>
        JsonSerializer.Serialize(writer, value, options);
}