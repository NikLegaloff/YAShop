using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageStore.Infrastructure;

namespace ImageStoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ImageStoreRegistry.Init(new ConsoleCommonInfrastructureProvider());

            string select = null;
            do
            {
                //Console.Clear();
                Console.WriteLine("Main menu");
                Console.WriteLine("1. Add new root folder");
                Console.WriteLine("2. Add new folder 2 level");
                Console.WriteLine("3. Add new folder 3 level");
                Console.WriteLine("4. ");
                Console.WriteLine("5. ");
                Console.WriteLine("0. Exit");
                select = Console.ReadLine();

                switch (select)
                {
                    case "1":
                        ImageStoreRegistry.Current.Folders.AddFolder("Folder num.1","");
                        break;
                    case "2":
                        ImageStoreRegistry.Current.Folders.AddFolder("Folder 2 level", "Folder num.1");
                        break;
                    case "3":
                        ImageStoreRegistry.Current.Folders.AddFolder("Folder 3 level", "Folder num.1\\Folder 2 level");
                        break;

                }
            } while (select != "0");

        }
    }
}
