using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Domain.Checkout;

namespace YAShop.Common.Domain
{
    public enum OrderStatus
    {
        Created,
        Paid,
        Shipped,
        Completed,
        Cancelled
    }

    public class OrderItem : DomainObject
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int QTY { get; set; }
    }

    public class Order : DomainObject
    {
        public Order()
        {
            Status=OrderStatus.Created;
        }


        public DateTime Date { get; set; }
        public OrderClientDetails  Details { get; set; }
        public string ClientEmail { get; set; }
        public OrderStatus Status { get; set; }
        public decimal SubTotal{ get; set; }
        public decimal Shipping{ get; set; }
        public decimal Total{ get; set; }
    }
}
