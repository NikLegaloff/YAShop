using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class ProductDataProvider : IDataProvider<Product>
    {
        public ProductDataProvider(IDataProvider<Product> executor) : base(executor) { }

        public List<Product> SelectFor(Category category)
        {
            return Select(" where CategoryId=@categoryId", new { categoryId = category.Id });
        }
    }
}