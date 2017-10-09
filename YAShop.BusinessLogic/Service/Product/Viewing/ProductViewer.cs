using System.Dynamic;
using System.Threading.Tasks;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Service.Product.Viewing
{
    public class ProductViewer
    {
        private readonly ProductFilter _filter;

        public ProductViewer(ProductFilter filter)
        {
            _filter = filter;
        }

        public async Task<PageData<ProductViewRow>> SelectPage()
        {
            var query =
                "select Product.Id, Product.SKU, Product.Title, Product.Image, Product.QTY, Product.Price, Category.[Name] Category from Product left join [dbo].[Category] on Product.CategoryId=Category.Id where 1=1 ";
            dynamic param = new ExpandoObject();
            if (!string.IsNullOrWhiteSpace(_filter.Keyword))
            {
                query += " and (Product.SKU = @Keyword or Product.Title like '%' + @Keyword )";
                param.Keyword = _filter.Keyword;
            }
            if (_filter.CategoryId != null)
            {
                query += " and Product.CategoryId=@CategoryId";
                param.CategoryId = _filter.CategoryId;
            }
            return await new MSSqlDataProvider<ProductViewRow>().SelectPage(query, _filter, param);
        }
    }
}