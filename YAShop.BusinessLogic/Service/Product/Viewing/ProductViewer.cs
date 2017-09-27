using System.Dynamic;
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

        public PageData<ProductViewRow> SelectPage()
        {
            dynamic param = new ExpandoObject();
            var query = "select Product.Id, Product.SKU, Product.Title, Product.Image, Product.QTY, Product.Price, Category.[Name] Category from Product left join [dbo].[Category] on Product.CategoryId=Category.Id where 1=1 ";
            if (!string.IsNullOrWhiteSpace(_filter.Keyword))
            {
                query += " and (Product.SKU = @Keyword or Product.Title like @Keyword +'%')";
                param.Keyword = _filter.Keyword;
            }
            if (_filter.CategoryId != null)
            {
                query += " and Product.CategoryId=@CategoryId";
                param.CategoryId = _filter.CategoryId;
            }
            return new MSSqlDataProvider<ProductViewRow>().SelectPage(query, _filter,param);
        }
    }
}