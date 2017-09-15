using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Sprut.MyShop
{
    public interface IDataProvider
    {
        Product GetProduct(string sku);
        Product[] GetAllProducts();
    }

    class TestProductProvider : IDataProvider
    {
        List<Product> _products = new List<Product>();

        

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

        public Product GetProduct(string sku)
        {
            return _products.FirstOrDefault(p => p.SKU == sku);
        }

        public Product[] GetAllProducts()
        {
            // Возваращаем массив что бы никто не 
            return _products.ToArray();
        }
    }
}