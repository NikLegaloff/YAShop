using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprut.MyShop
{
    class OrderProvider : IOrderProvider
    {
        readonly Order _order = new Order(); //пустой заказ

        // Добавить Product в заказ
        public void AddInOrder(string sku,int Qty)
        {
            Product product = Get(sku);
            OrderItem orderitem = new OrderItem();
            orderitem.SKU = product.SKU;
            orderitem.Title = product.Title;
            orderitem.Qty = Qty;
            orderitem.Price = product.Price * Qty;

            _order.Items.Add(orderitem);

        }

        // Получить список Productов
        public Order GetOrder()
        {
            return _order;
        }
    }
}