using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class OrderDataProvider : DataProvider<Order>
    {
        private int _counter = 0;
        public OrderDataProvider(IDataProvider<Order> executor) : base(executor) { }

        public Guid CreateOrder(Cart cart)
        {
            var order = new Order
            {
                Number = GetNewOrderNumber(),
                Date = DateTime.Now,
                State = OrderState.Created
            };
            foreach (var item in cart.Items)
            {
                var orderItem = new OrderItem
                {
                    SKU = item.SKU,
                    Title = item.Title,
                    Qty = item.Qty,
                    Price = Registry.Current.Products.GetProduct(item.SKU).Price,
                };
                order.Items.Add(orderItem);
                var product = Registry.Current.Products.GetProduct(item.SKU);
                product.Qty -= item.Qty;
                Registry.Current.Products.Save(product);
            }
            order.ItemsJSON = JsonConvert.SerializeObject(order.Items);
            Registry.Current.Orders.Save(order);
            Registry.Current.Carts.Clear();
            return order.Id;
        }

        public Order GetOrder(string number)
        {
            var order = Select("WHERE Number=@number", new {number}).First();
            order.Items = JsonConvert.DeserializeObject<List<OrderItem>>(order.ItemsJSON);
            return order;
        }

        public string GetNewOrderNumber()
        {
            return (((int)DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds) * 10 + _counter++ % 10).ToString();
        }
    }
}