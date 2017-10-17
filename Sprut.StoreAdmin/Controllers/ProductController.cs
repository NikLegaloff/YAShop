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
using System.Text.RegularExpressions;

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

            if (_pvModel.FilterTitle != null)
            {
                if (_pvModel.FilterTitle.StartsWith("SKU:"))
                {
                    _pvModel.FilterTitle = _pvModel.FilterTitle.Replace("SKU:", "").Trim();
                    conditions.Add("SKU LIKE @Text");
                }
                else
                {
                    if (_pvModel.FilterTitle.StartsWith("DESC:"))
                    {
                        _pvModel.FilterTitle = _pvModel.FilterTitle.Replace("DESC:", "").Trim();
                        conditions.Add("Descripton LIKE @Text");
                    }
                    else
                    {
                        conditions.Add("Title LIKE @Text");
                    }
                }
                param.Text = "%" + _pvModel.FilterTitle.Trim().Replace("  ", " ").Replace(" ", "%") + "%";
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
        public ActionResult Edit(Guid? id)
        {
            if (id!=null)
            {
                Product product = Registry.Current.Products.Find((Guid)id);
                _apvModel.ProductDTO.Id = product.Id;
                _apvModel.ProductDTO.CategoryId = product.CategoryId;
                _apvModel.ProductDTO.Descripton = product.Descripton;
                _apvModel.ProductDTO.Image = product.Image;
                _apvModel.ProductDTO.Price = product.Price;
                _apvModel.ProductDTO.Qty = product.Qty;
                _apvModel.ProductDTO.SKU = product.SKU;
                _apvModel.ProductDTO.Title = product.Title;
            }
            else
            {
                _apvModel.ProductDTO = new AddProductDTO();
            }
            return View(_apvModel);

        }

        [HttpPost]
        public ActionResult Edit(AddProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                _apvModel.ProductDTO = model.ProductDTO;
                var product= _apvModel.ProductDTO.Id != null ? Registry.Current.Products.Find(_apvModel.ProductDTO.Id.Value) :new Product();
                product.CategoryId = _apvModel.ProductDTO.CategoryId;
                product.Descripton = _apvModel.ProductDTO.Descripton;
                product.Image = _apvModel.ProductDTO.Image;
                product.Price = _apvModel.ProductDTO.Price;
                product.Qty = _apvModel.ProductDTO.Qty;
                product.SKU = _apvModel.ProductDTO.SKU;
                product.Title = _apvModel.ProductDTO.Title;
                Registry.Current.Products.Save(product);
            }
            else
            {
                return View(_apvModel);
            }
            return Redirect("/Product/Index");
        }

        public ActionResult Delete(Guid id)
        {
            Registry.Current.Products.Delete(Registry.Current.Products.Find(id));
            return Redirect("/Product/Index");
        }

        public ActionResult GroupAction()
        {
            var action = Request.Form["action"];
            //var selectedproducts = Request.Form["selectedproducts"];
            //selectedproducts = Regex.Replace(selectedproducts, ",", "','");
            //param.selectedproducts = $"'" + selectedproducts + "'";

            //I'm very cool )))
            var selectedproducts = $"'" + Regex.Replace(Request.Form["selectedproducts"], ",", "','") + "'";
            var listProduct = Registry.Current.Products.Select($"where Id in (" + selectedproducts + ")");

            switch (action)
            {
                case "delete":
                    //var listProduct = Registry.Current.Products.Select($"where Id in ("+selectedproducts+")");
                    foreach (var p in listProduct)
                    {
                        Registry.Current.Products.Delete(Registry.Current.Products.Find(p.Id));
                    }
                    break;
                case "remove":
                    //var listProduct = Registry.Current.Products.Select($"where Id in (" + selectedproducts + ")");
                    foreach (var p in listProduct)
                    {
                        var product = Registry.Current.Products.Find(p.Id);
                        var temp = Request.Form["moveCategory"];
                        var newCat = Guid.Parse(Request.Form["moveCategory"]);
                        product.CategoryId = newCat;
                        Registry.Current.Products.Save(product);
                    }
                    break;
                default:
                    break;
            }
            return Redirect("/Product/Index");

        }

    }
}