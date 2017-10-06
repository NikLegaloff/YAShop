using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop.Domain;

namespace Sprut.StoreAdmin.Models
{
    public class ProductViewModel
    {
        public int CurrentPage;

        //filter
        public string Order;
        public string CategoryId;
        public string Name;
        public string MinPrice;
        public string MaxPrice;

        
        public ProductViewModel()
        {
            CurrentPage = 1;
        }
    }

    
}