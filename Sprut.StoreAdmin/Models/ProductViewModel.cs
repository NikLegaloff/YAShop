using System.Web.Mvc;

namespace Sprut.StoreAdmin.Models
{
    public class ProductViewModel
    {
        public SelectList CategorySelectList;
        public int CurrentPage;


        public ProductViewModel()
        {
            CurrentPage = 1;
            FilterSort = "SKU";
            //if (FilterCategoryId.IsNullOrWhiteSpace()) FilterCategoryId = "1";
        }

        //filter
        public string FilterSort { get; set; }
        public int FilterSortDirection { get; set; }
        public string FilterCategoryId { get; set; }
        public string FilterTitle { get; set; }
        public string FilterMinPrice { get; set; }
        public string FilterMaxPrice { get; set; }
    }
}