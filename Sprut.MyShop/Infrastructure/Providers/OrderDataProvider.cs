using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class OrderDataProvider : DataProvider<Order>
    {
        private int _counter = 0;
        public OrderDataProvider(IDataProvider<Order> executor) : base(executor) { }

        public Guid CreateOrder(Cart cart)
        {
            decimal total = 0;
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
                    Order = order
                };
                total += (item.Qty + orderItem.Price);
                Registry.Current.Products.GetProduct(item.SKU).Qty -= item.Qty;
                //Registry.Current.Orders.(orderItem);
                Registry.Current.Orders.
            }
            Registry.Current.Orders.Save(order);
            Registry.Current.Carts.Clear();
            return order.Id;
        }


        public Order GetOrder(string number)
        {
            return Select("select * from Order WHERE Number=@number", new {number}).First();
        }

        public string GetNewOrderNumber()
        {
            return (((int)DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds) * 10 + _counter++ % 10).ToString();
        }
    }
}