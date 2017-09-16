using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Service
{

    public class OrderService : AbstractService
    {
        private int counter = 0;

        public Guid CreateAndSave(Order order)
        {
            order.State = OrderState.Created;
            order.Date = DateTime.Now;
            order.Number = GetNewOrderNumber();
            Registry.Current.Data.Orders.Save(order);
            foreach (var item in order.Items)
            {
                Registry.Current.Services.Product.Take(item.SKU, item.QTY);
            }
            Registry.Current.Services.Cart.Clear();
            return order.Id;
        }

        public decimal GetDiscountForAmount(decimal amount)
        {
            if (amount > 100) return 5;
            return 0;
        }

        public Order GetOrder(Cart cart)
        {
            var order = new Order();
            foreach (var cartItem in cart.Items)
            {
                var item = new OrderItem
                {
                    SKU = cartItem.SKU,
                    Title = cartItem.Title,
                    QTY = cartItem.QTY,
                    Price = Registry.Current.Services.Product.GetPrice(cartItem.SKU, cartItem.QTY)

                };
                order.Items.Add(item);
            }
            order.Discount = GetDiscountForAmount(order.Total);
            return order;
        }

        public string GetNewOrderNumber()
        {
            return (((int)DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds) * 10 + counter++ % 10).ToString();
        }
    }
}
