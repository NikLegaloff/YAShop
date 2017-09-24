using System;
using YAShop.BusinessLogic.Presistense.MSSQL;

namespace YAShop.BusinessLogic.Service.Product.Viewing
{
    public class ProductFilter : PagingArgs
    {
        public string Keyword{ get; set; }
        public Guid? CategoryId{ get; set; }
    }
}