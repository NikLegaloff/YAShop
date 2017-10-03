using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprut.MyShop.Domain
{
    [Table("Category")]
    public class Category : DomainObject
    {
        public Guid? ParentId { get; set; }
        public string Name { get; set; }
    }

    [NotMapped]
    public class CategoryTreeItem
    {
        public Guid Id { get; set; }
        public int Level { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public List<CategoryTreeItem> Childrens { get; set; }
    }
}
