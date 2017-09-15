using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sprut.MyShop
{
    public class DataProviders
    {
        private static DataProviders _current;
        public static DataProviders Current => _current ?? (_current = new DataProviders());

        public DataProviders()
        {
            Products = new TestProductProvider();
        }

        public IProductProvider Products { get; private set; }

    }
    public interface IProductProvider
    {
        Product Get(string sku);
        void Add(Product product);
        Product[] GetAll();
    }

    class TestProductProvider : IProductProvider
    {
        readonly List<Product> _products = new List<Product>();

        public TestProductProvider()
        {
            _products.Add(new Product
            {
                SKU = "S1",
                Title = "Test product 1",
                Price = 12.5m,
                Qty = 5,
                Descripton = "Product 1 description<br>with html"
            });
            _products.Add(new Product
            {
                SKU = "S2",
                Title = "Test product 2",
                Price = 12.0m,
                Qty = 10,
                Descripton = "Product 2 description<br>with html"
            });

        }

        public Product Get(string sku)
        {
            return _products.FirstOrDefault(p => p.SKU == sku);
        }

        public Product[] GetAll()
        {
            // ¬озваращаем массив что бы никто не изменил его
            return _products.ToArray();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }
    }
}