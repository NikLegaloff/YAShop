using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using Sprut.MyShop.Infrastructure;

namespace Sprut.MyShop.Domain.Model
{
    [Table("Product")]
    public class Product : DomainObject
    {
        [Required (ErrorMessage = "Field SKU can not be empty.")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "Field Title can not be empty.")]
        public string Title { get; set; }
        [Display(Name = "Price one qty")]
        public decimal Price { get; set; }
        public int Qty { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Descripton { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category => Registry.Current.Categories.Find(CategoryId);
    }
}