using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprut.MyShop
{
    class OrderProvider : IOrderProvider
    {
        readonly Order _order = new Order(); //пустой заказ

        //делаем новый заказ
        public OrderProvider()
        {
            _order.Id = 13; //тестовый номер
            _order.State = OrderState.Created;
            _order.Items = new List<OrderItem>();
            _order.Date = DateTime.Today;
        }

        // Добавить Product в заказ
        public void AddInOrder(string sku,int Qty)
        {
            Product product = DataProviders.Current.Products.Get(sku);
            OrderItem orderitem = new OrderItem();
            orderitem.SKU = product.SKU;
            orderitem.Title = product.Title;
            orderitem.Qty = Qty;
            orderitem.Price = product.Price;

            _order.Items.Add(orderitem);

        }

        // Получить список Productов
        public Order GetOrder()
        {
            return _order;
        }
    }
}