using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus;
using YAShop.BusinessLogic.Bus.Products;
using YAShop.BusinessLogic.DomainModel;
using YAShop.BusinessLogic.Presistense.MSSQL;
using YAShop.BusinessLogic.Service.Category;


namespace YAShop.ConsoleApp
{
    class Program
    {
        static  void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());
            DateTime now = DateTime.Now;
            foreach (var command in Registry.Current.Data.Commands.Select().Result) Registry.Current.Data.Commands.Delete(command);
            

            for (int i = 1; i <= 10; i++) Registry.Current.Bus.Invoke(new CancelOldNotPaidOrdersCommand { MaxDurationHours = i }).Wait();
            Console.ReadLine();
            do
            {
                bool result = new CommandExecutor().TakeOneAndExecute().Result;
                // if nothing processed
                if (!result) Thread.Sleep(2000); // then sleep 2 sec
                
            } while (true);

            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            Console.ReadLine();

        }

        private static void CreateInventoryAndCategories()
        {
            for (int i = 0; i < 100; i++)
            {
                Registry.Current.Data.Categories.Save(new Category
                {
                    Name = "Cat#" + i
                });
            }
            var cats = Registry.Current.Data.Categories.Select().Result;
            for (int i = 1; i <= 1000; i++)
            {
                Registry.Current.Data.Products.Save(new Product
                {
                    SKU = "S" + i,
                    Title = "Product #" + i + " title",
                    Category = cats[i % 100],
                    Price = i * 2 + 100,
                    QTY = i % 10,
                    Image = "https://images-na.ssl-images-amazon.com/images/I/81WnBU1Z0OL._SY355_.jpg",
                    Description = @"ASP.NET MVC gives you a powerful, patterns-based way to build dynamic websites that
                    enables a clean separation of concerns and gives you full control over markup
                    for enjoyable, agile development."
                });
            }
        }
    }
}
