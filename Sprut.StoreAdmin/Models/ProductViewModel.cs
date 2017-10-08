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
    public class ProductViewModel
    {
        public int CurrentPage { get; set; }
        public SelectList CategorySelectList;

        //filter
        public string FilterSort { get;set; }
        public int FilterSortDirection { get; set; }
        public string FilterCategoryId { get; set; }
        public string FilterTitle { get; set; }
        public string FilterMinPrice { get; set; }
        public string FilterMaxPrice { get; set; }
        public string FilterDescription { get; set; }


        public ProductViewModel()
        {
            CurrentPage = 1;
            FilterSort = "SKU";
            CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "Id", "FullName");

            //if (FilterCategoryId.IsNullOrWhiteSpace()) FilterCategoryId = "1";
        }
    }

    
}