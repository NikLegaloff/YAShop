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
using System.Dynamic;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly ProductViewModel _pvModel = new ProductViewModel();


        public ActionResult Index(ProductViewModel model)
        {
            _pvModel.CurrentPage = model.CurrentPage;
            List<string> conditions = new List<string>();
            dynamic param= new ExpandoObject();

            //delete
            var query = "";
            //????
            //if (!_pvModel.FilterCategoryId.IsNullOrWhiteSpace() | !_pvModel.FilterTitle.IsNullOrWhiteSpace() | !_pvModel.FilterMinPrice.IsNullOrWhiteSpace() | !_pvModel.FilterMaxPrice.IsNullOrWhiteSpace() )
            //{
            //    query += " WHERE ";
            //}

            //category :TODO
            if (!_pvModel.FilterCategoryId.IsNullOrWhiteSpace())
            {
                conditions.Add($"CategoryId=@CategoryId");
                //param.CategoryId = _pvModel.FilterCategoryId;
            }

            //name
            if (!_pvModel.FilterTitle.IsNullOrWhiteSpace())
            {
                conditions.Add($"Title=@Title");
                //param.Title = _pvModel.FilterTitle;
            }

            //price min
            //if (!_pvModel.FilterMinPrice.IsNullOrWhiteSpace())
            //{
            //    if (query.Contains("Title")) query += " AND ";
            //    query += string.Concat("Price >", _pvModel.FilterMinPrice);
            //}

            ////price max
            //if (!_pvModel.FilterMaxPrice.IsNullOrWhiteSpace())
            //{
            //    if (query.Contains("Price >")) query += " AND ";
            //    query += string.Concat("Price <", _pvModel.FilterMaxPrice, " ");
            //}

            //build query an param
            if (conditions.Count > 0)
            {
                query += " WHERE ";
                query += string.Join(" AND ", conditions.ToArray());
            }
            List<Product> products = Registry.Current.Products.Select(query);
            ViewBag.Count = products.Count;
            products.Clear();

            //order
            query += " ORDER BY @Sort";
            param.Sort = _pvModel.FilterSort;

            //offset-fetch
            query += " OFFSET @Offset ROWS FETCH NEXT 10 ROWS ONLY";
            param.Offset = (_pvModel.CurrentPage - 1) * 10;

            products = Registry.Current.Products.Select(query,param);
            ViewBag.Products = products;

           // _pvModel.CategorySelectList = new SelectList(Registry.Current.Categories.GetPlanarTree(), "FullName", "Name");

            //foreach (var c in Registry.Current.Categories.GetPlanarTree())
            //{
            //    //if(c.Name==pvModel.FilterCategoryId) pvModel.CategorySelectList.Add(new SelectListItem { Text = c.FullName, Value = c.Name, Selected = true});
            //    pvModel.CategorySelectList.Add(new SelectListItem{Text = c.FullName, Value = c.Name});
            //}
            
            return View(_pvModel);
        }

  



    }
}