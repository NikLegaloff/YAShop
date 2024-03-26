using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YAShop.Common.Service.Order
{
    public class OrderService
    {
        public Guid CreateOrder(Cart.Cart cart)
        {
            var order = new Domain.Order();
            // TODO: Implement order creation

            Registry.Current.Orders.Save(order);
            return Guid.Empty;
        }
    }
}
