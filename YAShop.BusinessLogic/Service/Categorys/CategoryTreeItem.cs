using System;
using System.Collections.Generic;

namespace YAShop.BusinessLogic.Service.Categorys
{
    public class CategoryTreeItem : AbstractService
    {
        public Guid Id { get; set; }
        public int Lvl { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<CategoryTreeItem> Childrens { get; set; }
    }
}