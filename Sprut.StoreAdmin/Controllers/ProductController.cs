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
using Sprut.MyShop.Domain.Model;
using System.Windows.Forms;
using Sprut;
using Sprut.StoreAdmin.ImageService;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        private ProductViewModels _pvModel = new ProductViewModels();
        private AddProductViewModel _apvModel = new AddProductViewModel();
        readonly ServiceClient _imageStoreRepository = new ServiceClient();



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
            foreach (var product in products)
            {
                if(product.Image!=null) product.Image = _imageStoreRepository.GetTmbUrl(Guid.Parse(product.Image));
            }
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
                if (product.Image != null) _apvModel.ProductDTO.Image = _imageStoreRepository.GetImageUrl(Guid.Parse(product.Image));
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
                //This working only local, but not on the server, i think.
                if (_apvModel.ProductDTO.Image != null)
                {
                    byte[] imageBytes = System.IO.File.ReadAllBytes(_apvModel.ProductDTO.Image);
                    product.Image=_imageStoreRepository.UploadImage(imageBytes, _apvModel.ProductDTO.Image.Split('\\').LastOrDefault(), "").ToString();
                }
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

        private static List<Product> ArrayToListProducts(string[] selectedItems)
        {
            var idStrings = $"'" + string.Join("','", selectedItems) + "'";
            var selectedproducts = Registry.Current.Products.Select($"where Id in (" + idStrings + ")");
            return selectedproducts;
        }

        public void Delete(string[] selectedItems)
        {
            foreach (var p in ArrayToListProducts(selectedItems))
            {
                Registry.Current.Products.Delete(Registry.Current.Products.Find(p.Id));
            }
        }
        
        public void SetQty(string[] selectedItems, int qty)
        {
            foreach (var p in ArrayToListProducts(selectedItems))
            {
                var product = Registry.Current.Products.Find(p.Id);
                product.Qty = qty;
                Registry.Current.Products.Save(product);
            }

        }

        public void ChangePrice(string[] selectedItems, int price)
        {
            foreach (var p in ArrayToListProducts(selectedItems))
            {
                var product = Registry.Current.Products.Find(p.Id);
                product.Price = price;
                Registry.Current.Products.Save(product);
            }

        }
    }
}