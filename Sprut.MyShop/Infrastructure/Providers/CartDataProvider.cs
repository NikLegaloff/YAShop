using System.Linq;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class CartDataProvider
    {
        public Cart GetCart()
        {
            var cart = (Cart) Registry.Current.CommonInfrastructureProvider.GetFromSession("Cart");
            if (cart == null)
            {
                cart = new Cart();
                Registry.Current.CommonInfrastructureProvider.PutInSession("Cart", cart);
            }
            return cart;
        }

        public void Add(string sku, string title, int qty)
        {
            var items = GetCart().Items;
            var existing = items.FirstOrDefault(i => i.SKU == sku);
            if (existing != null) existing.Qty += qty;
            else items.Add(new CartItem {SKU = sku, Title = title, Qty = qty});
        }

        public void Remove(string sku)
        {
            GetCart().Items.RemoveAll(i => i.SKU == sku);
        }

        public void Clear()
        {
            GetCart().Items.Clear();
        }
    }
}