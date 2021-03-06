using System.Collections.Generic;
using System.Linq;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Domain.Model;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class ProductDataProvider : DataProvider<Product>
    {
        public ProductDataProvider(IDataProvider<Product> executor) : base(executor)
        {
        }

        public List<Product> SelectFor(Category category)
        {
            return Select(" where CategoryId=@categoryId", new {categoryId = category.Id});
        }

        public Product GetProduct(string sku)
        {
            return Select("select * from Product WHERE SKU=@sku", new {sku}).First();
        }
    }
}