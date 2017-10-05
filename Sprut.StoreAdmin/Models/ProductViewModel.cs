using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprut.StoreAdmin.Models
{
    public class ProductViewModel
    {
        public int CurrentPage;
        public string Order;

        public ProductViewModel()
        {
            CurrentPage = 1;
            Order = "SKU";
        }
    }

    
}