using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public class Cart
    {
        public int Id { get; set; } //надо ли !!!!
        public List<CartItem> Items { get; set; }
        public decimal Total => Items.Sum(s => s.Price * s.Qty);

        
    }

   
    public class CartItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
