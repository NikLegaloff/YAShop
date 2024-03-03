using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Domain;

namespace YAShop.Common.Data
{
    public class ProductDataProvider
    {
        public Product[] SelectAll()
        {
            var all = new List<Product>();
            for (int i = 10; i <= 30; i++)
                all.Add(new Product
                {
                    Id = Guid.Parse("e4aaf240-8f6a-4485-b040-594c579425" + i), 
                    SKU = "P-" + i, 
                    Title = $"Product #{i} title",
                    Price = 10+i*2.5m, 
                    QTY = i-1,
                    Image = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Canon_EOS_50D.jpg",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
                });
            
            return all.ToArray();
        }
    }
    public  class Registry
    {
        private static Registry? _current;
        public static Registry Current => _current ??= new Registry();
        
        public ProductDataProvider Products { get; private set; }

        public Registry()
        {
            Products = new ProductDataProvider();
        }
    }
}
