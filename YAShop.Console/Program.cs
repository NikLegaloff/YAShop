using System;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus.Products;


namespace YAShop.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());

            
            var cartService = Registry.Current.Services.Cart;
            var products = Registry.Current.Data.Products.Select(product => true);
            var now = DateTime.Now;

            for (int i = 0; i < 1000000; i++)
            {
                Registry.Current.Bus.Execute(new AddProductToCartCommand { SKU = products[0].SKU, Title = products[0].Title, QTY = 1 });
            }
            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            Console.ReadLine();
            return;
            cartService.Add(products[0].SKU, products[0].Title, 1);
            cartService.Add(products[1].SKU, products[1].Title, 1);

            var order = Registry.Current.Services.Order.GetOrder(cartService.GetCart());
            Registry.Current.Services.Order.CreateAndSave(order);
            System.Console.WriteLine(order.Total + "$");
            cartService.Add(products[0].SKU, products[0].Title, 2);
            cartService.Add(products[1].SKU, products[1].Title, 2);
            order = Registry.Current.Services.Order.GetOrder(cartService.GetCart());
            System.Console.WriteLine(order.Total + "$");
            Registry.Current.Services.Order.CreateAndSave(order);
            Console.WriteLine("---------------------------------");
            foreach (var oo in Registry.Current.Data.Orders.Select(order1 => true))
            {
                Console.WriteLine(oo.ToString());
            }


            System.Console.ReadLine();

        }
    }
}
