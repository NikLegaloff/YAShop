using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    public enum OrderState
    {
        Created, Paid, Shipped, Delivered
    }

    public class Order : DomainObject
    {
        public string Number { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public decimal Total => Items.Sum(s => s.Price * s.Qty);


        public Order()
        {
            Items = new List<OrderItem>();
        }
    }


    public class OrderItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
    }
}
