using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YAShop.Common.Domain;

namespace YAShop.Common.Data
{
    public class ProductDataprovider
    {
        public Product[] SelectAll()
        {
            var all = new List<Product>();
            for (int i = 1; i <= 20; i++)
                all.Add(new Product
                {
                    Id = Guid.NewGuid(), 
                    SKU = "P-" + i, 
                    Title = $"Product #{i} title",
                    Price = 10+i*2.5m, 
                    QTY = i-1,
                    Image = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Canon_EOS_50D.jpg"
                });
            
            return all.ToArray();
        }
    }
    public  class Registry
    {
        private static Registry? _current;
        public static Registry Current => _current ??= new Registry();
        
        public ProductDataprovider Products { get; private set; }

        public Registry()
        {
            Products = new ProductDataprovider();
        }
    }
}
