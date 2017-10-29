using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Bus;
using YAShop.BusinessLogic.Bus.Commands;
using YAShop.BusinessLogic.DomainModel;
using YAShop.ImagesClient;
using YAShop.ImagesClient.Images;

namespace YAShop.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Registry.Init(new ProgrCommonInfrProvider());
            ProcessImages();
            return;
            var now = DateTime.Now;
            Console.WriteLine("i - create inv and commands");
            Console.WriteLine("* - clear commands");
            Console.WriteLine("0 - start commands processing");
            Console.WriteLine("1-10 - invoke N commands");
            Console.WriteLine("img - run images console");
            do
            {
                var c = Console.ReadLine();
                switch (c)
                {
                    
                    case "l": CreateListings(); break;
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

        private static void ProcessImages()
        {
            var client = new ImagesHostClient("http://localhost:9011/");
            /*
            for (int i = 0; i < 5; i++)
            {
                var folder = client.AddFolder("Folder" + i, null);
                for (int j = 0; j < 3; j++)
                {
                    client.AddFolder("SubFolder" + i + "-" + j, folder);
                }
            }
            foreach (var folder in foldersTree)
            {
                Console.WriteLine(folder.Name);
                foreach (var subFolder in folder.Childrens)
                {
                    Console.WriteLine("\t" + subFolder.Name);
                }
            }*/
            Folder[] foldersTree = client.GetFoldersTree();
            foreach (var file in Directory.GetFiles("O:\\1\\"))
            {
                var now = DateTime.Now;
                var bytes = File.ReadAllBytes(file);
                FileInfo f = new FileInfo(file);
                var imageId = client.UploadImage(f.Name, bytes, foldersTree[0].Id);
                var tt = DateTime.Now.Subtract(now).TotalSeconds;
                File.AppendAllText("C:\\Img\\Log2.txt",tt + "\r\n");

            }
        }

        private static void CreateListings()
        {
            Guid id1 = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            var start = DateTime.Now;
            List<Guid> ids = new List<Guid>();
            for (int i = 0; i < 100; i++)
            {
                var listing = new Listing
                {
                    Description = "asdf sdfjsdfj sdf sdf wef wef wefe f';ef 'ejf ejfk jwefl jer klqejrl kqer fqerf qer qer fqerf " + i,
                    Name = "Listing number " + i,
                    Number = i,
                    ProductId = id1,
                    UserId = id2
                };
                Registry.Current.Data.Listings.Save(listing);
                ids.Add(listing.Id);
            }
            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds + " create");
            start = DateTime.Now;
            for (int i=0; i<100; i++)
            {
                Registry.Current.Data.Listings.Select(l => l.Number<i);
            }
            Console.WriteLine(DateTime.Now.Subtract(start).TotalMilliseconds + " selects");
            start = DateTime.Now;
            
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