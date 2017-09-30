using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    public enum OrderState
    {
        Created, Paid, Shipped, Delivered
    }
    [Table("Order")]
    public class Order : DomainObject
    {
        public string Number { get; set; }
        //public List<OrderItem> Items { get; set; }
        public virtual ICollection<OrderItem> Items { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public decimal Total => Items.Sum(s => s.Price * s.Qty);
        
        public Order()
        {
            Items = new List<OrderItem>();
        }
    }

    [Table("OrderItem")]
    public class OrderItem
    {
        [Key]
        [ForeignKey("Order")]
        public string Number { get; set; }
        public string Title { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}
