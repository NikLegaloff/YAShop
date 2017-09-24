using System;
using System.Collections.Generic;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus.Products;
using YAShop.BusinessLogic.DomainModel;
using YAShop.BusinessLogic.Presistense.MSSQL;


namespace YAShop.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());
            var orders = Registry.Current.Data.Orders.SelectPage(" where state=1",new PagingArgs {RowsPerPage = 100,Page = 1,Sort = "Number",SortDir = SortDir.ASC});
            DateTime now = DateTime.Now;
            foreach (var order1 in orders.Rows)
            {
                Registry.Current.Data.Orders.Save(order1);
            }
            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            Console.ReadLine();
            return;
            var products = Registry.Current.Data.Products.Select();




            var cartService = Registry.Current.Services.Cart;

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
