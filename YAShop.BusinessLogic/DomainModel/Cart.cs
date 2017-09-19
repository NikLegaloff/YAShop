using System.Collections.Generic;

namespace YAShop.BusinessLogic.DomainModel
{
    public class CartItem
    {
        public CartItem(string sku, string title, int qty)
        {
            SKU = sku;
            Title = title;
            QTY = qty;
        }

        public CartItem() { }

        public string SKU { get; set; }
        public string Title { get; set; }
        public int QTY { get; set; }
    }
    public class Cart
    {
        public Cart()
        {
            Items = new List<CartItem>();
        }

        public List<CartItem> Items { get; set; }
    }
}