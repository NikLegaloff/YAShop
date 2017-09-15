using System.Collections.Generic;
using System.Linq;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Service
{
    public class CartService : AbstractService
    {
        public void Remove(string sku)
        {
            GetCart().Items.RemoveAll(i => i.SKU == sku);
        }

        public void Add(string sku, string title, int qty)
        {
            var items = GetCart().Items;
            var existing = items.FirstOrDefault(i => i.SKU == sku);
            if (existing != null) existing.QTY += qty;
            else items.Add(new CartItem { SKU = sku, Title = title, QTY = qty });
        }

        public Cart GetCart()
        {
            var cart = (Cart)Registry.Current.Infrastructure.Common.GetFromSession("Cart");
            if (cart == null)
            {
                cart = new Cart();
                Registry.Current.Infrastructure.Common.PutInSession("Cart", cart);
            }
            return cart;
        }

        public void Clear()
        {
            GetCart().Items.Clear();
        }
    }
}