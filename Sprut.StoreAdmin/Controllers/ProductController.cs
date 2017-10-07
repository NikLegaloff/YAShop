using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Sprut.MyShop;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure;
using Sprut.StoreAdmin.Models;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductViewModel _pvModel = new ProductViewModel();

        public ActionResult Index(ProductViewModel m)
        {
            //change filter field on Model
            //pvModel.FilterCategoryId = Request.Form["fCategory"];
            //pvModel.FilterTitle = Request.Form["fName"];
            //pvModel.FilterMinPrice = Request.Form["fPriceMin"];
            //pvModel.FilterMaxPrice = Request.Form["fPriceMax"];
            //pvModel.FilterSort = !Request.Form["fSort"].IsNullOrWhiteSpace() ? Request.Form["fSort"] : "SKU";

            //if (page == null)
            //{
            //    pvModel.CurrentPage = 1;
            //}
            //else
            //{
            //    pvModel.CurrentPage = (int)page;
            //}

            var query = "";

            if (!_pvModel.FilterCategoryId.IsNullOrWhiteSpace() | !_pvModel.FilterTitle.IsNullOrWhiteSpace() | !_pvModel.FilterMinPrice.IsNullOrWhiteSpace() | !_pvModel.FilterMaxPrice.IsNullOrWhiteSpace() )
            {
                query += " WHERE ";
            }

            //category :TODO

            //name
            if(!_pvModel.FilterTitle.IsNullOrWhiteSpace()) query += string.Concat("Title LIKE '%", _pvModel.FilterTitle, "%'");

            //price min
            if (!_pvModel.FilterMinPrice.IsNullOrWhiteSpace())
            {
                if (query.Contains("Title")) query += " AND ";
                query += string.Concat("Price >", _pvModel.FilterMinPrice);
            }

            //price max
            if (!_pvModel.FilterMaxPrice.IsNullOrWhiteSpace())
            {
                if (query.Contains("Price >")) query += " AND ";
                query += string.Concat("Price <", _pvModel.FilterMaxPrice, " ");
            }

            List<Product> products = Registry.Current.Products.Select(query);
            ViewBag.Count = products.Count;
            products.Clear();
            
            //order;
            query += string.Concat(" ORDER BY ",_pvModel.FilterSort);

            //offset-fetch
            query += string.Concat(" OFFSET ", (_pvModel.CurrentPage-1) * 10, " ROWS FETCH NEXT 10 ROWS ONLY");

            products = Registry.Current.Products.Select(query);
            ViewBag.Products = products;

            _pvModel.CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "FullName", "Name");

            //foreach (var c in Registry.Current.Categories.GetPlanarTree())
            //{
            //    //if(c.Name==pvModel.FilterCategoryId) pvModel.CategorySelectList.Add(new SelectListItem { Text = c.FullName, Value = c.Name, Selected = true});
            //    pvModel.CategorySelectList.Add(new SelectListItem{Text = c.FullName, Value = c.Name});
            //}
            
            return View(_pvModel);
        }

  



    }
}