using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure;
using Sprut.StoreAdmin.Models;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly FilterViewModels _filter = new FilterViewModels();

        public ActionResult Index(FilterViewModels _filter)
        {
            List<Product> products = Registry.Current.Products.Select().ToList();

            if (_filter.Name != null)
            {
                products = products.FindAll(p => p.Title.Contains(_filter.Name)==true);
            }

            switch (_filter.Sort)
            {
                case 1:
                    products = products.OrderBy(p=>p.Price).ToList();
                    break;
                case 2:
                    products = products.OrderByDescending(p => p.Price).ToList();
                    break;
            }

            if (_filter.minPrice > 0)
            {
                products = products.FindAll(p => p.Price > _filter.minPrice);
            }

            if (_filter.maxPrice > 0)
            {
                products = products.FindAll(p => p.Price < _filter.maxPrice);
            }

            ViewBag.Products = products;
            //TODO: implement it in another place, not Index()
            //ViewBag.Categorys = CategoryProviders.Current.Category.GetTree();
            return View(_filter);
        }

  


        [HttpGet]
        public ActionResult Edit(string sku)
        {
            Product product;
            if (sku==null)
            {
                product = new Product();
                ViewBag.Action = "Добавить";
            }
            else
            {
                product = Registry.Current.Products.GetProduct(sku);
                ViewBag.Action = "Изменить";
            }
            return View("Edit", product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            Registry.Current.Products.Save(product);
            return Redirect("Index");
        }
        public ActionResult DeleteProduct(string sku)
        {
            Registry.Current.Products.Delete(Registry.Current.Products.GetProduct(sku));
            return Redirect("Index");

        }

        [HttpGet]
        public ActionResult ImportXLS()
        {
            //TODO: implement it in another place, not Index()

            /*
            var xlArray = Registry.Current.Products.ImportFromExcel("d:\\temp\\MyShopTest.xlsx");
            ViewBag.xlArray = xlArray;
            ViewBag.xlArrayRows = xlArray.GetLength(0);
            ViewBag.xlArrayCols = xlArray.GetLength(1);
            */
            return View();
        }
        [HttpPost]
        public ActionResult ImportXLS(string temp)
        {
            //TODO: implement it in another place, not Index()
            /*
            //для теста добавления в базу, по идее нужно с представления возвращать данные для добавления
            var xlArray = Registry.Current.Products.ImportFromExcel("d:\\temp\\MyShopTest.xlsx");
            for(int i = 1; i < xlArray.GetLength(0); i++)
            {
                Product _product_temp = new Product();
                _product_temp.SKU = xlArray[i, 0];
                _product_temp.Title = xlArray[i, 1];
                _product_temp.Price = Decimal.Parse(xlArray[i, 2]);
                _product_temp.Qty = Int16.Parse(xlArray[i, 3]);
                _product_temp.Image = xlArray[i, 4];
                _product_temp.Descripton = xlArray[i, 5];
                //_product.CategoryId = Guid.Parse(xlArray[i, 6]); не решено с категорией

                Registry.Current.Products.Save(_product_temp);
            }*/
            return Redirect("Index");
        }

        public ActionResult Category()
        {
          //  ViewBag.Categorys = Registry.Current.Categories.GetTree();
            return View();
        }

        [HttpGet]
        public ActionResult CatEdit(string guid, string act)
        {
            Category _category;

            Guid _parse;
            Guid.TryParse(guid, out _parse);

            if (act == "new")
            {
                _category = new Category()
                {
                    Id = Guid.NewGuid(),
                    ParentId = _parse
                };
                ViewBag.Action = "Добавить";
            }
            else
            {

                _category = Registry.Current.Categories.Find(_parse);
                ViewBag.Action = "Изменить";
            }
            return View("CatEdit", _category);
        }
        [HttpPost]
        public ActionResult CatEdit(Category category)
        {
            Registry.Current.Categories.Save(category);
            return Redirect("Category");
        }

    }
}