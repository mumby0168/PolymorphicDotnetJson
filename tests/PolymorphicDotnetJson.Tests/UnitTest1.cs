using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace PolymorphicDotnetJson.Tests;

public class OrderStream : EventStream
{
    
}

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
}