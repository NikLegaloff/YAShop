using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprut.StoreAdmin.Models
{
    public class FilterViewModels
    {
        public Guid Category { get; set; }
        public string Name { get; set; }
        public int Sort { get; set; }
        public decimal minPrice { get; set; }
        public decimal maxPrice { get; set; }


        //public FilterViewModels()
        //{
        //    Name = "";
        //}
    }

    
}