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
            DataProviders.Current.Products.Add(new Product {SKU = "New SKu 1", Title = "New title", Price = 111.11m});
            var allProducts = DataProviders.Current.Products.GetAll();
            foreach (var product in allProducts)
            {
                Console.WriteLine(product.SKU + "\t" + product.Title + "\t" + product.Price.ToString("0.00"));
            }

            Console.ReadLine();
        }
    }
}
