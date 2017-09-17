using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.MyShop;

namespace Sprut.MyShopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProviders.Current.Products.Add(new Product {SKU = "NewSKu1", Title = "New title", Price = 111.11m});

            string select = null;
            do {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. View catalog");
                Console.WriteLine("2. Bay");
                Console.WriteLine("3. View shoping cart");
                Console.WriteLine("0. Exit");
                select=Console.ReadLine();

                switch (select)
                {
                    case "1":
                        var allProducts = DataProviders.Current.Products.GetAll();
                        Console.WriteLine("SKU \t Title \t\t Price \t Description");
                        foreach (var product in allProducts)
                        {
                            
                            Console.WriteLine(product.SKU + "\t " + product.Title + "\t " + product.Price.ToString("0.00")+"\t "+product.Descripton);
                        };
                        break;
                    case "2":
                        Console.Write("Enter SKU for Baying:");
                        var sku=Console.ReadLine();
                        Console.Write("Enter Qty:");
                        var qty = Convert.ToInt16(Console.ReadLine());
                        CartProviders.Current.Cart.AddInCart(sku, qty);
                        break;
                    case "3":
                        Cart cart = CartProviders.Current.Cart.GetCart();
                        Console.WriteLine("SKU \t Title \t\t Qty \t Price (1Qty) \t Total price");

                        foreach (var item in cart.Items)
                        {
                            Console.WriteLine(item.SKU + " \t " + item.Title + "\t " 
                                + item.Qty+" \t"+item.Price+"\t\t "+item.Price*item.Qty);


                        }
                        break;


                }
            } while (select != "0");




        }
    }
}
