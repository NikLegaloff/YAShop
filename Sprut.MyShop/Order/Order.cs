using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop
{
    public enum OrderState
    {
        Created, Paid, Shipped, Delivered
    }

    public class Order
    {
        public int Id { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public decimal Total => Items.Sum(s => s.Price * s.Qty);

        public Order()
        {
            Id = 1; //тестовый номер
            State = OrderState.Created;
            Items = new List<OrderItem>();
            Date = DateTime.Today;
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
