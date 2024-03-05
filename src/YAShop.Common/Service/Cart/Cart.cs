using Newtonsoft.Json;

namespace YAShop.Common.Service.Cart;

public class Cart
{
    public Cart()
    {
    }

    public List<CartItem> Items { get; set; } = new();

    [JsonIgnore]
    public bool IsEmpty => TotalCount == 0;
    [JsonIgnore] 
    public int TotalCount => Items.Sum(i => i.QTY);
    [JsonIgnore] public decimal TotalAmount => Items.Sum(i => i.Price * i.QTY);
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public string Title{ get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int QTY { get; set; }
}