using System;
using System.ComponentModel.DataAnnotations.Schema;
using Sprut.MyShop.Infrastructure;

namespace Sprut.MyShop.Domain
{
    [Table("Product")]
    public class Product : DomainObject
    {
        public string SKU { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Image { get; set; }
        public string Descripton { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category => Registry.Current.Categories.Find(CategoryId);
    }
}