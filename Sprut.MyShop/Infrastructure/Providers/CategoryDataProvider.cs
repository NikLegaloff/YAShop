using System.Collections.Generic;
using Sprut.MyShop.Domain;
using System;
using System.Linq;

namespace Sprut.MyShop.Infrastructure.Providers
{
    public class CategoryDataProvider : DataProvider<Category> 
    {
        public CategoryDataProvider(IDataProvider<Category> executor) : base(executor){}

        public List<CategoryTreeItem> GetTree()
        {
            return new CategoryTreeBuilder(Registry.Current.Categories.Select(" order by name").ToArray()).Build();
        }
        public List<CategoryTreeItem> GetPlanarTree()
        {
            var tree = new CategoryTreeBuilder(Registry.Current.Categories.Select(" order by name").ToArray()).Build();
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


    public class CategoryTreeBuilder
    {
        private readonly Category[] _categories;

        public CategoryTreeBuilder(Category[] categories)
        {
            _categories = categories;
        }

        public List<CategoryTreeItem> Build()
        {
            var treeItems = DoBuild(null, 1, "");
            return treeItems;
        }

        private List<CategoryTreeItem> DoBuild(Guid? parentId, int level, string parentName)
        {
            return _categories.Where(p => p.ParentId == parentId).Select(c =>
            {
                var fullName = (string.IsNullOrWhiteSpace(parentName) ? "" : parentName + " > ") + c.Name;
                return new CategoryTreeItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    Level = level,
                    FullName = fullName,
                    Childrens = DoBuild(c.Id, level + 1, fullName)
                };
            }).ToList();
        }
    }
}