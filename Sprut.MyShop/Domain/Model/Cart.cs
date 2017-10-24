using System.Collections.Generic;

namespace Sprut.MyShop.Domain.Model
{
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
        public CartItem()
        {
        }

        public CartItem(string sku, string title, int qty)
        {
            SKU = sku;
            Title = title;
            Qty = qty;
        }

        public string SKU { get; set; }
        public string Title { get; set; }
        public int Qty { get; set; }
    }
}