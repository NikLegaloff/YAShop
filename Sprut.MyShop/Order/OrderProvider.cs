using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprut.MyShop
{
    class OrderProvider : IOrderProvider
    {
        readonly Order _order = new Order(); //пустой заказ

        //делаем новый заказ
        public string StartOrder(Cart cart)
        {
            _order.Number = (((int)DateTime.Now.Subtract(new DateTime(2017, 9, 15)).TotalSeconds) * 10).ToString(); //тестовый номер
            _order.Items = new List<OrderItem>();
            _order.Date = DateTime.Now;
            _order.State = OrderState.Created;

            foreach(var item in cart.Items)
            {
                _order.Items.Add(new OrderItem{ SKU = item.SKU, Title=item.Title, Qty=item.Qty, Price=item.Price});
            }
            return _order.Number;
        }

        // Получить заказ номер... (возвращает пока только текущий)
        public Order GetOrder(string number)
        {
            return _order;
        }

        // Изменить статус заказа
        public void SetOrderState(string number,OrderState state)
        {
            _order.State = state;
        }
    }
}