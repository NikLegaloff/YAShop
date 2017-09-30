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
                    Price = Registry.Current.Products.GetProduct(item.SKU).Price
                };
                order.Items.Add(orderItem);
                Registry.Current.Products.GetProduct(item.SKU).Qty -= item.Qty;
            }
            Registry.Current.Orders.Save(order);
            Registry.Current.Carts.Clear();
            return order.Id;
        }


        public Order GetOrder(string number)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter("number", number));

            const string sql = "select * from Order WHERE Number=@number";
            return Select(sql, param.ToArray()).First();
        }

        public string GetNewOrderNumber()
        {
            return (((int)DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds) * 10 + _counter++ % 10).ToString();
        }
    }
}