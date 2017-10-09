using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class OrderDataProvider : DataProvider<Order>
    {
        private int _counter;

        public OrderDataProvider(IDataProvider<Order> executor) : base(executor)
        {
        }


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
                var product = Registry.Current.Products.GetProduct(item.SKU);
                product.Qty -= item.Qty;
                Registry.Current.Products.Save(product);
            }
            Registry.Current.Orders.Save(order);
            Registry.Current.Carts.Clear();
            return order.Id;
        }

        public Order GetOrder(string number)
        {
            var order = Select("WHERE Number=@number", new {number}).First();
            return order;
        }

        public string GetNewOrderNumber()
        {
            return
                ((int) DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds * 10 + _counter++ % 10).ToString();
        }

        protected override void AfterLoad(Order order)
        {
            order.Items = JsonConvert.DeserializeObject<List<OrderItem>>(order.ItemsJSON);
        }

        protected override void BeforeSave(Order order)
        {
            order.ItemsJSON = JsonConvert.SerializeObject(order.Items);
        }
    }
}