using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.MyShop;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure;

namespace Sprut.MyShopConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry.Init(new ConsoleCommonInfrastructureProvider());

            //Registry.Current.Products.Save(new Product {SKU = "NewSKu1", Title = "New title", Price = 111.11m});

            //string order = null;

            var products = Registry.Current.Products.Select(" where QTY=@qty and SKU=@sku", new {qty = 0, sku= "NewSKu1" });


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
                Console.WriteLine("9. Add from Excel");
                Console.WriteLine("10. Build Category Tree");
                Console.WriteLine("11. Delete SKU");
                Console.WriteLine("0. Exit");
                select=Console.ReadLine();

                switch (select)
                {
                    case "1":
                        var allProducts = Registry.Current.Products.Select();
                        Console.WriteLine("SKU \t Title \t\t Price \t Description");
                        foreach (var product in allProducts)
                        {
                            
                            Console.WriteLine(product.SKU + "\t " + product.Title + "\t " + product.Price.ToString("0.00")+"\t "+product.Descripton);
                        };
                        break;
                    case "2":
                        Console.Write("Enter SKU for Baying:");
                        var sku = Console.ReadLine();
                        Console.Write("Enter Qty:");
                        var qty = Convert.ToInt16(Console.ReadLine());
                        var title = Registry.Current.Products.GetProduct(sku).Title;
                        Registry.Current.Carts.Add(sku,title,qty);
                        break;
                    case "3":
                        Cart cart = Registry.Current.Carts.GetCart();
                        Console.WriteLine("SKU \t Title \t\t Qty \t Price (1 Qty) \t Total price");
                        decimal summary = 0;
                        foreach (var item in cart.Items)
                        {
                            var itemPrice = Registry.Current.Products.GetProduct(item.SKU).Price;
                            Console.WriteLine(item.SKU + " \t " + item.Title + "\t "
                                + item.Qty + " \t" + itemPrice + "\t\t " + itemPrice * item.Qty);
                            summary += itemPrice * item.Qty;
                        }
                        Console.WriteLine("Total: " + summary);
                        break;
                    case "4":
                        Console.Write("Enter SKU for Delete:");
                        var skuDel = Console.ReadLine();
                        Registry.Current.Carts.Remove(skuDel);
                        break;
                    //case "5":
                    //    //order = OrderProviders.Current.Order.StartOrder(CartProviders.Current.Cart.GetCart());
                    //    break;
                    //case "6":
                    //    //var torder = OrderProviders.Current.Order.GetOrder(order);
                    //    Console.WriteLine("Order number: " + torder.Number + ", date: " + torder.Date + ", state:" + torder.State);
                    //    Console.WriteLine("SKU \tTitle \t\tQty \tPrice");
                    //    foreach(var item in torder.Items)
                    //    {
                    //        Console.WriteLine(item.SKU + "\t" + item.Title + "\t" + item.Qty + "\t" + item.Price);
                    //    }
                    //    Console.WriteLine("Total: " + torder.Items.Sum(s => s.Price * s.Qty));
                    //    break;
                    case "7":
                        var product2 = Registry.Current.Products.GetProduct("NewSKu1");
                        Console.WriteLine("SKU \t Title \t\t Price \t Description");
                        Console.WriteLine(product2.SKU + "\t " + product2.Title + "\t " + product2.Price.ToString("0.00") + "\t " + product2.Descripton);
                        break;
                    //case "8":
                    //    //Registry.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");
                    //    break;
                    //case "9":
                    //        //для теста добавления в базу, по идее нужно с представления возвращать данные для добавления
                    //        //var xlArray = Registry.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");

                    //        for (int i = 1; i < xlArray.GetLength(0); i++)
                    //        {
                    //        Product _product_temp = new Product();
                    //        _product_temp.SKU = xlArray[i, 0];
                    //            _product_temp.Title = xlArray[i, 1];
                    //            _product_temp.Price = Decimal.Parse(xlArray[i, 2]);
                    //            _product_temp.Qty = Int16.Parse(xlArray[i, 3]);
                    //            _product_temp.Image = xlArray[i, 4];
                    //            _product_temp.Descripton = xlArray[i, 5];
                    //            //_product.CategoryId = Guid.Parse(xlArray[i, 6]); не решено с категорией

                    //            //Registry.Current.Products.Add(_product_temp);
                    //        }
                    //        break;
                    // case "10":
                    //    //var categorytree = CategoryProviders.Current.Category.GetTree();
                    //    //foreach(var cat in categorytree)
                    //    {
                    //        Console.WriteLine(cat.Name + "\t" + cat.Id);
                    //    }
                    //    break;
                    case "11":
                        Console.WriteLine("Delete SKU:");
                        var skufordel = Console.ReadLine();
                        Registry.Current.Products.Delete(Registry.Current.Products.GetProduct(skufordel));
                        break;


                }
            } while (select != "0");




        }
    }
}
