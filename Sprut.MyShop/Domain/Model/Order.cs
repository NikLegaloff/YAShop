﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;

namespace Sprut.MyShop.Domain.Model
{
    public enum OrderState
    {
        Created,
        Paid,
        Shipped,
        Delivered
    }

    [Table("Order")]
    public class Order : DomainObject
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public string Number { get; set; }

        [NotMapped]
        public List<OrderItem> Items { get; set; }

        //public virtual List<OrderItem> Items { get; set; }
        public DateTime Date { get; set; }
        public OrderState State { get; set; }
        public string ItemsJSON { get; set; }
        public decimal Total => Items.Sum(s => s.Price * s.Qty);
    }

    [DataContract]
    public class OrderItem : DomainObject
    {
        [DataMember]
        public string SKU { get; set; }

        [DataMember]
        public string Title { get; set; }

        [DataMember]
        public int Qty { get; set; }

        [DataMember]
        public decimal Price { get; set; }
    }
}