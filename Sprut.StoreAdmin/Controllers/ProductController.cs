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
        readonly ProductViewModel pvModel = new ProductViewModel();

        public ActionResult Index(int? page)
        {
            //change filter field on Model
            pvModel.CategoryId = Request.Form["fCategory"];
            pvModel.Name = Request.Form["fName"];
            pvModel.MinPrice = Request.Form["fPriceMin"];
            pvModel.MaxPrice = Request.Form["fPriceMax"];
            pvModel.Order = !Request.Form["fSort"].IsNullOrWhiteSpace() ? Request.Form["fSort"] : "SKU";

            if (page == null)
            {
                pvModel.CurrentPage = 1;
            }
            else
            {
                pvModel.CurrentPage = (int)page;
            }

            var query = "";

            if (!pvModel.CategoryId.IsNullOrWhiteSpace() | !pvModel.Name.IsNullOrWhiteSpace() | !pvModel.MinPrice.IsNullOrWhiteSpace() | !pvModel.MaxPrice.IsNullOrWhiteSpace() )
            {
                query += " WHERE ";
            }

            //category :TODO

            //name
            if(!pvModel.Name.IsNullOrWhiteSpace()) query += string.Concat("Title LIKE '%", pvModel.Name, "%'");

            //price min
            if (!pvModel.MinPrice.IsNullOrWhiteSpace())
            {
                if (query.Contains("Title")) query += " AND ";
                query += string.Concat("Price >", pvModel.MinPrice);
            }

            //price max
            if (!pvModel.MaxPrice.IsNullOrWhiteSpace())
            {
                if (query.Contains("Price >")) query += " AND ";
                query += string.Concat("Price <", pvModel.MaxPrice, " ");
            }

            List<Product> products = Registry.Current.Products.Select(query);
            ViewBag.Count = products.Count;
            products.Clear();
            
            //order;
            query += string.Concat(" ORDER BY ",pvModel.Order);

            //offset-fetch
            query += string.Concat(" OFFSET ", (pvModel.CurrentPage-1) * 10, " ROWS FETCH NEXT 10 ROWS ONLY");

            products = Registry.Current.Products.Select(query);
            ViewBag.Products = products;

            return View(pvModel);
        }

  



    }
}