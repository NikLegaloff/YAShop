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
        private ProductViewModels _pvModel = new ProductViewModels();
        private AddProductViewModel _apvModel = new AddProductViewModel();


        public ActionResult Index(ProductViewModels model)
        {
            _pvModel = model;
            var conditions = new List<string>();
            dynamic param= new ExpandoObject();
            var query = "";
            
            //category
            if (!_pvModel.FilterCategoryId.IsNullOrWhiteSpace())
            {
                conditions.Add("CategoryId=@CategoryId");
                param.CategoryId = _pvModel.FilterCategoryId;
            }

            //name
            if (!_pvModel.FilterTitle.IsNullOrWhiteSpace())
            {
                conditions.Add("Title LIKE '%@Title%'");
                param.Title = _pvModel.FilterTitle;
            }

            //description
            if (!_pvModel.FilterDescription.IsNullOrWhiteSpace())
            {
                conditions.Add("Descripton LIKE '%@Descrition%'");
                param.Description = _pvModel.FilterDescription;
            }
            
            //build query an param
            if (conditions.Count > 0)
            {
                query += " WHERE ";
                query += string.Join(" AND ", conditions.ToArray());
            }
            List<Product> products = Registry.Current.Products.Select(query,param);
            ViewBag.Count = products.Count;
            products.Clear();

            //order
            if (_pvModel.FilterSort.Contains(" ")) throw new Exception("Space char in sort column");
            query += $" ORDER BY "+_pvModel.FilterSort;
            if (_pvModel.FilterSortDirection == "1") query += " DESC";

            //offset-fetch
            query += " OFFSET @Offset ROWS FETCH NEXT 10 ROWS ONLY";
            param.Offset = (_pvModel.CurrentPage - 1) * 10;

            products = Registry.Current.Products.Select(query,param);
            ViewBag.Products = products;
            return View(_pvModel);
        }
        [HttpGet]
        public ActionResult Add(string sku)
        {
            if (!sku.IsNullOrWhiteSpace())
            {
                _apvModel.Product = Registry.Current.Products.GetProduct(sku);
            }
            else
            {
                _apvModel.Product = new Product();
            }
            return View(_apvModel);

        }

        [HttpPost]
        public ActionResult Add(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                _apvModel.Product = model.Product;
                Registry.Current.Products.Save(_apvModel.Product);
            }
            else
            {
                return View(_apvModel);
            }
            return Redirect("Index");
        }



    }
}