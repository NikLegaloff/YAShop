namespace YAShop.Common.Service.Cart;

public class Cart
{
    public Cart()
    {
        Items = new List<CartItem>();
    }

    public List<CartItem> Items { get; set; }

    public bool IsEmpty => TotalCount == 0;
    public int TotalCount => Items.Sum(i => i.QTY);
    public decimal TotalAmount => Items.Sum(i => i.Price * i.QTY);
}

public class CartItem
{
    public Guid ProductId { get; set; }
    public string Title{ get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public int QTY { get; set; }
}