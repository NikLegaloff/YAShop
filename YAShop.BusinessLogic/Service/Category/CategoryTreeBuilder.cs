using System;
using System.Collections.Generic;
using System.Linq;

namespace YAShop.BusinessLogic.Service.Category
{
    public class CategoryTreeBuilder
    {
        private readonly  DomainModel.Category[] _categories;

        public CategoryTreeBuilder(DomainModel.Category[] categories)
        {
            _categories=categories;
        }

        
        public List<CategoryTreeItem> Build()
        {
            var treeItems = DoBuild(null, 1, "");
            return treeItems;
        }

        private List<CategoryTreeItem>  DoBuild(Guid? parentId, int lvl, string parentName)
        {
            return _categories.Where(p => p.ParentId == parentId) .Select(c =>
            {
                var fullName = (string.IsNullOrWhiteSpace(parentName) ? "" : parentName + " > ") + c.Name;
                return new CategoryTreeItem
                {
                    Id = c.Id,
                    Name = c.Name,
                    Lvl = lvl,
                    FullName = fullName,
                    Childrens = DoBuild(c.Id, lvl + 1, fullName)
                };
            }).ToList();
        }
    }
}