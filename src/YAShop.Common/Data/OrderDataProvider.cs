using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class OrderDataProvider : FileJsonDataProvider<Order>
{
    public OrderDataProvider(string? dataPath = null) : base(dataPath)
    {
    }
}

public class OrderItemDataProvider : FileJsonDataProvider<OrderItem>
{
    public OrderItemDataProvider(string? dataPath = null) : base(dataPath)
    {
    }
}