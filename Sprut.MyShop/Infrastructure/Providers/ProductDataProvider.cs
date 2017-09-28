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

        public Product GetProduct(string sku)
        {
            var sql = "SELECT * FROM Products WHERE SKU=@sku";
           // danger code, possible sql injection 
            // var sql = "SELECT * FROM Products WHERE SKU='" + sku + "'";
            return Select(sql,new {sku}).First();
        }

        // needless
        /*   public void DeleteProduct(string sku)
           {
               var subj = Registry.Current.Products.GetProduct(sku);
               Registry.Current.Products.Delete(subj);
           }
                   public List<Product> GetList()
           {
               //var param = new List<ObjectParameter>();
               //param.Add(new ObjectParameter("name","value"));

               //ObjectParameter[] param = new[]{new ObjectParameter("Name", "any value")};
               //System.Data.SqlClient.SqlParameter param = new System.Data.SqlClient.SqlParameter("@name", "%Samsung%");
               return Select("SELECT * FROM Products");
           }

        */
    }
}