using System.Collections.Generic;

namespace YAShop.BusinessLogic.Service.Category
{
    public class CategoryService : AbstractService
    {
        public List<CategoryTreeItem> GetTree()
        {
            var categories = AsyncHelpers.RunSync(() => Registry.Current.Data.Categories.Select(" order by name"));
            return new CategoryTreeBuilder(categories).Build();
        }

        public List<CategoryTreeItem> GetPlanarTree()
        {
            var categories = AsyncHelpers.RunSync(() => Registry.Current.Data.Categories.Select(" order by name"));
            var tree = new CategoryTreeBuilder(categories).Build();
            var list = new List<CategoryTreeItem>();
            AddItems(list, tree);
            return list;
        }

        private void AddItems(List<CategoryTreeItem> list, List<CategoryTreeItem> tree)
        {
            foreach (var categoryTreeItem in tree)
            {
                list.Add(categoryTreeItem);
                if (categoryTreeItem.Childrens != null) AddItems(list, categoryTreeItem.Childrens);
                categoryTreeItem.Childrens = null;
            }
        }
    }
}