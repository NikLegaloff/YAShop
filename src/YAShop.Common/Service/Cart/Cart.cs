namespace YAShop.Common.Service.Cart;

public class Cart
{
    public Cart()
    {
        Items = new List<CartItem>();
    }

    public List<CartItem> Items { get; set; }
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public int QTY { get; set; }
}