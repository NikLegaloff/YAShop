using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure;

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
        public Product Product { get; set; }

        public AddProductViewModel()
        {
            Product=new Product();
            CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName");
        }
    }

    
}