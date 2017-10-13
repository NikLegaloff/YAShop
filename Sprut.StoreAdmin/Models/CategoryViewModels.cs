using Sprut.MyShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop.Domain;

namespace Sprut.StoreAdmin.Models
{
    public class CategoryViewModels
    {
        public List<CategoryTreeItem> CategoryTreeItems;
        public SelectList CategorySelectList;
        public Category Category { get; set; }

        public CategoryViewModels()
        {
            CategoryTreeItems = Registry.Current.Categories.GetPlanarTree();

            CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName");
        }
    }
    
}