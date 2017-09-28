using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public class Cart
    {
        public List<CartItem> Items { get; set; }

        public Cart()
        {
            Items =new List<CartItem>();
        }
    }


    public class CartItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int Qty { get; set; }

        public CartItem(){}

        public CartItem(string sku, string title, int qty)
        {
            SKU = sku;
            Title = title;
            Qty = qty;
        }
    }
}
