namespace PolymorphicDotnetJson;

public class ItemAddedToCart : Event
{
    public Guid OrderId { get; set; }
    
    public string ItemId { get; set; } = default!;

    public double Price { get; set; } = 10;

    public ItemAddedToCart()
    {
        EventName = "ItemAddedToCart";
    }
}