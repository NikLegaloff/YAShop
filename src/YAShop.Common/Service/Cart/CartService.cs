namespace YAShop.Common.Service.Cart;



public class CartService
{
    public void Add(Guid productId, int qty)
    {
        var cart = GetCart();
        var exist = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (exist == null)
        {
            var p = Registry.Current.Products.Find(productId);
            cart.Items.Add(new CartItem { ProductId = productId, QTY = qty, Price = p.Price, Title = p.Title,Image = p.Image});
        }
        else
            exist.QTY++;
        SaveCart(cart);
    }

    private void SaveCart(Cart cart)
    {
        cart.Items.RemoveAll(i => i.QTY == 0);
        Registry.Current.Infrastructure.PutInSession("Cart",cart);
    }
    public Cart GetCart()
    {
        return Registry.Current.Infrastructure.GetFromSession<Cart>("Cart")?? new Cart();
    }
}