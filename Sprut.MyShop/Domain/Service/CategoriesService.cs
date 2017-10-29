using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprut.MyShop.Domain.Model;
using Sprut.MyShop.Infrastructure;

namespace Sprut.MyShop.Domain.Service
{
    public class CategoriesService
    {
        public Guid GetCategoryIdByName(string name)
        {
            // но метод надо дорабатывать
            Category result = null;
            var categoryFullPathList = name.Split('>').ToList();
            Guid? parentCategoryId = null;
            foreach (var cat in categoryFullPathList)
            {
                if (GetListCategorys(cat, parentCategoryId).Count == 0)
                {
                    var newCategory = new Category()
                    {
                        Name = cat.Trim(),
                        ParentId = parentCategoryId
                    };
                    Registry.Current.Categories.Save(newCategory);
                    result = newCategory;
                }
                parentCategoryId = GetListCategorys(cat, parentCategoryId).First().Id;
            }
            return result.Id;
        }

        List<Category> GetListCategorys(string cat, Guid? parentCategoryId)
        {
            if (parentCategoryId == null)
                return Registry.Current.Categories.Select($"where Name='" + cat.Trim() +
                                                       "' and ParentId is null");
            else
                return Registry.Current.Categories.Select($"where Name='" + cat.Trim() + "' and ParentId='" +
                                                       parentCategoryId + "'");
        }
    }
}
