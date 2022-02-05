using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using BenchmarkDotNet.Attributes;
using Newtonsoft.Json;
using PolymorphicDotnetJson;
using PolymorphicDotnetJson.SystemText;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace BenchMarksJson;

[MemoryDiagnoser]
public class JsonBenchMarks
{
    private string TestData()
    {
        var streamOne = new OrderStream()
        {
            Payload = new ItemAddedToCart
            {
                Price = 10,
                ItemId = "123",
                OrderId = Guid.NewGuid()
            }
        };
    
        return JsonConvert.SerializeObject(streamOne);
    }
    
    [Benchmark]
    public void NewtonsoftJTokenReadFrom()
    {
        var data = TestData();
    
        var serializer = new JsonSerializer();
        serializer.Converters.Add(new EventReadFromConverter());
    
        using var stringReader = new StringReader(data);
        using var jsonTextReader = new JsonTextReader(stringReader);
    
        var orderStream = serializer.Deserialize<OrderStream>(jsonTextReader);
    }
    
    [Benchmark]
    public void NewtonsoftJTokenLoad()
    {
        var data = TestData();
    
        var serializer = new JsonSerializer();
        serializer.Converters.Add(new EventLoadConverter());
    
        using var stringReader = new StringReader(data);
        using var jsonTextReader = new JsonTextReader(stringReader);
    
        var orderStream = serializer.Deserialize<OrderStream>(jsonTextReader);
    }
    
    
    private JsonSerializerOptions _systemTextOptions = new JsonSerializerOptions()
    {
        Converters =
        {
            new SystemTextEventConverter()
        }
    };
    
    
    [Benchmark]
    public void SystemText()
    {
        var data = TestData();
    
        var orderStream = System.Text.Json.JsonSerializer.Deserialize<OrderStream>(data, _systemTextOptions);
    }
    

    private string TestData1()
    {
        var streamOne = new OrderStreamDtoBased()
        {
            EventInstance = new EventDto()
            {
                Payload = new ItemAddedToCart
                {
                    Price = 10,
                    ItemId = "123",
                    OrderId = Guid.NewGuid()
                },
                EventName = nameof(ItemAddedToCart)
            }
        };

        return JsonConvert.SerializeObject(streamOne);
    }

    private JsonSerializerOptions _options = new JsonSerializerOptions()
    {
        Converters = {new SystemTextEventDtoConverter()}
    };


    [Benchmark]
    public void EventDtoPayload()
    {
        var data = TestData1();
        var streamInstance = System.Text.Json.JsonSerializer.Deserialize<OrderStreamDtoBased>(data, _options);
    }
}