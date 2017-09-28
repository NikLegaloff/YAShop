using System.Collections.Generic;

namespace YAShop.BusinessLogic.Service.Category
{
    public class CategoryService : AbstractService
    {

        public List<CategoryTreeItem> GetTree()
        {
            return new CategoryTreeBuilder(Registry.Current.Data.Categories.Select(" order by name")).Build();
        }
    }
}