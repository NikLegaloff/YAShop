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
            string order = null;



            string select = null;
            do {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. View catalog");
                Console.WriteLine("2. Bay");
                Console.WriteLine("3. View shoping cart");
                Console.WriteLine("4. Delete from cart");
                Console.WriteLine("5. Start order");
                Console.WriteLine("6. View order");
                Console.WriteLine("7. Info S1");
                Console.WriteLine("8. Import from Excel");
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
                        Console.WriteLine("Total: " + cart.Items.Sum(s => s.Price * s.Qty));
                        break;
                    case "4":
                        Console.Write("Enter SKU for Delete:");
                        var sku_del = Console.ReadLine();
                        CartProviders.Current.Cart.DelFromCart(sku_del);
                        break;
                    case "5":
                        order = OrderProviders.Current.Order.StartOrder(CartProviders.Current.Cart.GetCart());
                        break;
                    case "6":
                        var torder = OrderProviders.Current.Order.GetOrder(order);
                        Console.WriteLine("Order number: " + torder.Number + ", date: " + torder.Date + ", state:" + torder.State);
                        Console.WriteLine("SKU \tTitle \t\tQty \tPrice");
                        foreach(var item in torder.Items)
                        {
                            Console.WriteLine(item.SKU + "\t" + item.Title + "\t" + item.Qty + "\t" + item.Price);
                        }
                        Console.WriteLine("Total: " + torder.Items.Sum(s => s.Price * s.Qty));
                        break;
                    case "7":
                        var product2 = DataProviders.Current.Products.Get("S1");
                        Console.WriteLine("SKU \t Title \t\t Price \t Description");
                        Console.WriteLine(product2.SKU + "\t " + product2.Title + "\t " + product2.Price.ToString("0.00") + "\t " + product2.Descripton);
                        break;
                    case "8":
                        DataProviders.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");
                        break;
                        

                }
            } while (select != "0");




        }
    }
}
