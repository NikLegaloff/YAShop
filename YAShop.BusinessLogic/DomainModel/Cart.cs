using System.Collections.Generic;

namespace YAShop.BusinessLogic.DomainModel
{
    public class CartItem
    {
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