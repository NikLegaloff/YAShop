using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace Sprut.StoreAdmin.Models
{
    public class ProductViewModels
    {
        public int CurrentPage { get; set; }
        public SelectList CategorySelectList;

        //filter
        public string FilterSort { get;set; }
        public string FilterSortDirection { get; set; }
        public string FilterCategoryId { get; set; }
        public string FilterTitle { get; set; }


        public ProductViewModels()
        {
            CurrentPage = 1;
            FilterSort = "SKU";
            FilterSortDirection = "0";
            CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName");
        }
    }

    public class AddProductViewModel
    {
        public SelectList CategorySelectList;
        public AddProductDTO ProductDTO { get; set; }

        public AddProductViewModel()
        {
            ProductDTO=new AddProductDTO();
            CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName");
        }
    }

    public class AddProductDTO
    {
        public Guid? Id { get; set; }
        [Required(ErrorMessage = "Field SKU can not be empty.")]
        public string SKU { get; set; }
        [Required(ErrorMessage = "Field Title can not be empty.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Field Price can not be empty.")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Field Qty can not be empty.")]
        public int Qty { get; set; }
        public string Image { get; set; }
        [AllowHtml]
        public string Descripton { get; set; }
        public Guid CategoryId { get; set; }
    }
}