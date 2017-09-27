using System.Collections.Generic;
using Sprut.MyShop.Domain;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class ProductDataProvider : DataProvider<Product>
    {
        public ProductDataProvider(IDataProvider<Product> executor) : base(executor) { }

        public List<Product> SelectFor(Category category)
        {
            return Select(" where CategoryId=@categoryId", new { categoryId = category.Id });
        }

<<<<<<< HEAD
        public List<Product> GetList()
        {
            //var param = new List<ObjectParameter>();
            //param.Add(new ObjectParameter("name","value"));

            //ObjectParameter[] param = new[]{new ObjectParameter("Name", "any value")};
            //System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", "%Samsung%");
            return Select("SELECT * FROM Products");
        }

        public Product GetProduct(string sku)
        {
            var sql = "SELECT * FROM Products WHERE SKU='" + sku + "'";
            return Select(sql).First();
        }

        public void DeleteProduct(string sku)
        {
            var subj = Registry.Current.Products.GetProduct(sku);
            Registry.Current.Products.Delete(subj);
        }

=======
        public Product FindBySKU(string sku)
        {
            throw new System.NotImplementedException();
        }
>>>>>>> 7a26487f3718ef600bb6d64216d7b4f5912c01c1
    }
}