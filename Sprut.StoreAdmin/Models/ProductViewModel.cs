using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sprut.MyShop.Domain;

namespace Sprut.StoreAdmin.Models
{
    public class ProductViewModel
    {
        public int CurrentPage;
        public SelectList CategorySelectList;

        //filter
        public string FilterSort;
        public int FilterSortDirection;
        public string FilterCategoryId;
        public string FilterTitle;
        public string FilterMinPrice;
        public string FilterMaxPrice;

        
        public ProductViewModel()
        {
            CurrentPage = 1;
            FilterSort = "SKU";
            //if (FilterCategoryId.IsNullOrWhiteSpace()) FilterCategoryId = "1";
        }
    }

    
}