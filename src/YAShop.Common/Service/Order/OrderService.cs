using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using YAShop.Common.Domain;
using YAShop.Common.Domain.Checkout;


namespace YAShop.Common.Service.Order
{
    public class OrderService
    {
        public decimal GetCityPriceById(Guid cityId)
        {
            var city = Registry.Current.Cities.Find(cityId);
           
            return city.Price; 
        }
        public Guid CreateOrder(Cart.Cart cart)
        {

            var cityPrice = GetCityPriceById(cart.CheckoutDetails.Address.CityId);
            var order = new Domain.Order
            {
                Date = DateTime.Now,
                ClientEmail = cart.CheckoutDetails.Email,
                Status = Domain.OrderStatus.Created,
                SubTotal = cart.TotalAmount,
                Shipping = cityPrice,
                Details = new OrderClientDetails { Address = cart.CheckoutDetails.Address,
                    FirstName = cart.CheckoutDetails.FirstName,
                    LastName = cart.CheckoutDetails.LastName,
                    Comment = cart.CheckoutDetails.Comment,
                },
                Total = cart.TotalAmount + cityPrice
               };
            Registry.Current.Orders.Save(order);
       
            // TODO: Implement order creation

            foreach (var i in cart.Items)
            {
                var orderItem = new Domain.OrderItem
                {
                    OrderId = order.Id,
                    ProductId = i.ProductId,
                    Title = i.Title,
                    Price = i.Price,
                    QTY = i.QTY
                };

                Registry.Current.OrderItems.Save(orderItem);

              
                
            }
           
            return Guid.Empty;
        }
    }
}
