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
    
    [Benchmark]
    public void SystemText()
    {
        var data = TestData();

        var options = new JsonSerializerOptions
        {
            Converters = {new SystemTextEventConverter()}
        };
        
        var orderStream = System.Text.Json.JsonSerializer.Deserialize<OrderStream>(data, options);
    }
    
    [Benchmark]
    public void SystemTextRaw()
    {
        var data = TestData();

        var options = new JsonSerializerOptions
        {
            Converters = {new SystemTextEventConverter()}
        };
        
        var orderStream = System.Text.Json.JsonSerializer.Deserialize<OrderStream>(data, options);
    }
}