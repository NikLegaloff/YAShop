using System;
using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class ProductDataProvider : DataProvider<Product>
    {
        public ProductDataProvider(IDataProvider<Product> executor) : base(executor) { }

        public List<Product> SelectFor(Category category)
        {
            return Select(" where CategoryId=@categoryId", new { categoryId = category.Id });
        }

        public Product FindBySKU(string sku)
        {
            var findBySku = Select(" where SKU=@sku", new {sku});
            if (findBySku!=null && findBySku.Count>0) return findBySku[0];
            throw new Exception("Not found with SKU " + sku);
        }
    }
}