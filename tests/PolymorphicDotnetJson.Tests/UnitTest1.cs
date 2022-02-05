using System;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PolymorphicDotnetJson.SystemText;
using Xunit;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace PolymorphicDotnetJson.Tests;



public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        //Arrange
        var streamOne = new OrderStream()
        {
            Payload = new ItemAddedToCart
            {
                Price = 10,
                ItemId = "123",
                OrderId = Guid.NewGuid()
            }
        };
    
        var json = JsonConvert.SerializeObject(streamOne);
    
        var returned = JsonConvert.DeserializeObject<OrderStream>(json);
    }

    [Fact]
    public void Test3()
    {
        //Arrange
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

        var options = new JsonSerializerOptions()
        {
            Converters = {new SystemTextEventDtoConverter()}
        };

        var json = JsonSerializer.Serialize(streamOne);

        var returned = JsonSerializer.Deserialize<OrderStreamDtoBased>(json, options);
    }
}