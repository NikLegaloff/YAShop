using System;
using System.Collections.Generic;
using System.Linq;
using YAShop.BusinessLogic.Presistense;

namespace YAShop.BusinessLogic.DomainModel
{
    public enum OrderState
    {
        Created, Paid, Shipped, Delivered
    }

    public class Order : DomainObject
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public string Number { get; set; }
        public List<OrderItem> Items { get; set; }
        public DateTime Date { get; set; }
        public decimal Discount { get; set; }
        public OrderState State { get; set; }
        public decimal Total => Items.Sum(i => i.Price * i.QTY) * (1 - Discount / 100m);
        //  written at altittude 9km :)
        public string DeliveryOptions { get; set; }

    }

    public class OrderItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int QTY { get; set; }
        public decimal Price { get; set; }
    }

}