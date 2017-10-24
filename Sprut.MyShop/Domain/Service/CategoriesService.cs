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
            Category result =null;
            var categoryFullPathList = name.Split('>').ToList();
            var parentCategoryId = Guid.Empty;
            if (Registry.Current.Categories.Select($"where Name='" + categoryFullPathList[0].Trim() + "' and ParentId is null").Count ==0)
            {
                var newCategory = new Category()
                {
                    Name = categoryFullPathList[0].Trim()
                };
                Registry.Current.Categories.Save(newCategory);
                result=newCategory;
            }
            parentCategoryId = Registry.Current.Categories.Select($"where Name='" + categoryFullPathList[0].Trim() + "' and ParentId is null").First().Id;
            categoryFullPathList.RemoveAt(0);
            
            foreach (var cat in categoryFullPathList)
            {
                if (Registry.Current.Categories.Select($"where Name='" + cat.Trim() + "' and ParentId='" + parentCategoryId + "'").Count == 0)
                {
                    var newCategory = new Category()
                    {
                        Name = cat.Trim(),
                        ParentId = parentCategoryId
                    };
                    Registry.Current.Categories.Save(newCategory);
                    result = newCategory;
                }
                parentCategoryId = Registry.Current.Categories.Select($"where Name='" + cat.Trim() + "' and ParentId='" + parentCategoryId + "'").First().Id;
            }
            return result.Id;
        }
    }
}
