using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using YAShop.BusinessLogic.Presistense;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.DomainModel
{
    public interface IWithEmbededProperty
    {
    
    }
    public enum OrderState
    {
        Created, Paid, Shipped, Delivered
    }

    public class Order : DomainObject, IWithEmbededProperty
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        [DBField(SqlDbType.NVarChar, 10)]
        public string Number { get; set; }
        [DBField(SqlDbType.DateTime)]
        public DateTime Date { get; set; }
        [DBField(SqlDbType.Decimal)]
        public decimal Discount { get; set; }
        [DBField(SqlDbType.Int)]
        public OrderState State { get; set; }
        public decimal Total => Items.Sum(i => i.Price * i.QTY) * (1 - Discount / 100m);
        //  written at altittude 9km :)
        [DBField(SqlDbType.NText)]
        public string DeliveryOptions { get; set; }

        [DBField(SqlDbType.NVarChar, 0, false, true, typeof(List<OrderItem>))]
        public List<OrderItem> Items { get; set; }
        private string ItemsJSON { get; set; }

    }

    public class OrderItem
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public int QTY { get; set; }
        public decimal Price { get; set; }
    }

}