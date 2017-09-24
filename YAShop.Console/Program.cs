using System;
using System.Collections.Generic;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus.Products;
using YAShop.BusinessLogic.DomainModel;
using YAShop.BusinessLogic.Presistense.MSSQL;
using YAShop.BusinessLogic.Service.Category;


namespace YAShop.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());
            DateTime now = DateTime.Now;
            var cats = Registry.Current.Data.Categories.Select();
            for (int i = 1; i <= 1000; i++)
            {
                Registry.Current.Data.Products.Save(new Product
                {
                    SKU = "S" + i,
                    Title = "Product #" + i + " title",
                    Category = cats[i%100],
                    Price = i*2+100,
                    QTY = i%10,
                    Image = "https://images-na.ssl-images-amazon.com/images/I/81WnBU1Z0OL._SY355_.jpg",
                    Description = @"ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that
                    enables a clean separation of concerns and gives you full control over markup
                    for enjoyable, agile development."
                });
            }
            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            Console.ReadLine();

        }

    }
}
