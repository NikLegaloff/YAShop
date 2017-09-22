using System.Collections.Generic;
using Sprut.MyShop.Domain;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class CategoryDataProvider : DataProvider<Category> 
    {
        public CategoryDataProvider(IDataProvider<Category> executor) : base(executor){}

        public List<Category> SelectRootLevel()
        {
            return Select(" where ParentId is null");
        }
    }
}