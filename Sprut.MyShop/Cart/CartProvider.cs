using System;
using System.Collections.Generic;
using System.Linq;

namespace Sprut.MyShop
{
    class CartProvider : ICartProvider
    {
        readonly Cart _cart = new Cart(); //пустая корзина

        //делаем новый заказ
        public CartProvider()
        {
            _cart.Id = 13; //тестовый номер
            _cart.Items = new List<CartItem>();
        }

        // Добавить Product в корзину
        public void AddInCart(string sku,int Qty)
        {
            Product product = DataProviders.Current.Products.Get(sku);
            CartItem cartitem = new CartItem();
            cartitem.SKU = product.SKU;
            cartitem.Title = product.Title;
            cartitem.Qty = Qty;
            cartitem.Price = product.Price;

            _cart.Items.Add(cartitem);

        }

        // Получить список Productов
        public Cart GetCart()
        {
            return _cart;
        }
    }
}