using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop;
using Sprut.StoreAdmin.Models;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        readonly FilterViewModels _filter = new FilterViewModels();

        public ActionResult Index(FilterViewModels _filter)
        {
            List<Product> products = DataProviders.Current.Products.GetAll().ToList();

            if (_filter.Name != "")
            {
                products = products.FindAll(p => p.Title.Contains(_filter.Name)==true);
            }

            switch (_filter.Sort)
            {
                case 1:
                    products = products.OrderBy(p=>p.Price);
                    break;
                case 2:
                    products = products.FindAll(p => p.Title.Contains(_filter.Name) == true);
                    break;
            }
           

            ViewBag.Products = products;
            ViewBag.Categorys = CategoryProviders.Current.Category.GetTree();
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
                product = DataProviders.Current.Products.Get(sku);
                ViewBag.Action = "Изменить";
            }
            return View("Edit", product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            DataProviders.Current.Products.Add(product);
            return Redirect("Index");
        }
        public ActionResult DeleteProduct(string sku)
        {
            DataProviders.Current.Products.Delete(sku);
            return Redirect("Index");

        }

        [HttpGet]
        public ActionResult ImportXLS()
        {
            var xlArray = DataProviders.Current.Products.ImportFromExcel("d:\\temp\\MyShopTest.xlsx");
            ViewBag.xlArray = xlArray;
            ViewBag.xlArrayRows = xlArray.GetLength(0);
            ViewBag.xlArrayCols = xlArray.GetLength(1);
            return View();
        }
        [HttpPost]
        public ActionResult ImportXLS(string temp)
        {
            //для теста добавления в базу, по идее нужно с представления возвращать данные для добавления
            var xlArray = DataProviders.Current.Products.ImportFromExcel("d:\\temp\\MyShopTest.xlsx");
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

                DataProviders.Current.Products.Add(_product_temp);
            }
            return Redirect("Index");
        }

    }
}