using System;
using System.Threading;
using System.Threading.Tasks;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus;
using YAShop.BusinessLogic.Bus.Commands;
using YAShop.BusinessLogic.DomainModel;

namespace YAShop.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());

            var now = DateTime.Now;
            Console.WriteLine("i - create inv and commands");
            Console.WriteLine("* - clear commands");
            Console.WriteLine("0 - start commands processing");
            Console.WriteLine("1-10 - invoke N commands");
            Console.WriteLine("(enter) - run api console");
            do
            {
                var c = Console.ReadLine();
                switch (c)
                {
                    case "i": CreateInventoryAndCategories().Wait(); break;
                    case "": RunConsole(); break;
                    case "0":
                        ProcessCommands();
                        break;
                    case "*":
                        foreach (var command in Registry.Current.Data.Commands.Select().Result)
                            Registry.Current.Data.Commands.Delete(command);
                        break;
                    default:
                        for (var i = 0; i < int.Parse(c); i++)
                            Registry.Current.Bus.Invoke(new CancelOldNotPaidOrdersCommand {MaxDurationHours = 2}).Wait();
                        break;
                }
            } while (true);
            Console.WriteLine(DateTime.Now.Subtract(now).TotalMilliseconds);
            Console.ReadLine();
        }

        // консоль пользует клиента
        // и вот как это работает
        // запустим исполнителя и консольку в режиме клиента
        private static void RunConsole()
        {
            Console.Clear();
            Console.Write("API URL(http://localhost:9004/):");
            var url = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(url)) url = "http://localhost:9004/";
            var client = new CommandApiClient(url);
            var exit = false;
            Console.WriteLine("");
            do
            {
                Console.Write(">>");
                var str = Console.ReadLine();
                if (str == "exit") return;
                var res = client.ExecuteCommand(str);
                Console.WriteLine(res);
            } while (true);
        }

        private static void ProcessCommands()
        {
            do
            {
                var result = new CommandExecutor(Console.WriteLine).TakeOneAndExecute().Result;
                // if nothing processed
                if (!result) Thread.Sleep(200); // then sleep 2 sec
            } while (true);
        }

        private static async Task CreateInventoryAndCategories()
        {
            for (var i = 0; i < 100; i++)
            {
                var category = new Category
                {
                    Name = "Cat#" + i
                };
                await Registry.Current.Data.Categories.Save(category);
            }
            var cats = Registry.Current.Data.Categories.Select().Result;
            for (var i = 1; i <= 1000; i++)
               await Registry.Current.Data.Products.Save(new Product
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