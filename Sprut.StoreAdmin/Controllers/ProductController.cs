using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sprut.MyShop;

namespace Sprut.StoreAdmin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewAllProduct()
        {
            ViewBag.Products = DataProviders.Current.Products.GetAll();
            return View();
        }


        public ActionResult ViewAllProductForEdit()
        {
            ViewBag.Products = DataProviders.Current.Products.GetAll();
            return View();
        }


        [HttpGet]
        public ActionResult EditProduct(string sku)
        {
            Product product = DataProviders.Current.Products.Get(sku);
            return View("EditProduct", product);
        }
        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            DataProviders.Current.Products.Add(product);
            return Redirect("ViewAllProductForEdit");
        }
        public ActionResult DeleteProduct(string sku)
        {
            DataProviders.Current.Products.Delete(sku);
            return Redirect("ViewAllProductForEdit");

        }

        [HttpGet]
        public ActionResult AddProduct()
        {
            Product product = new Product();
            return View("AddProduct", product);
        }
        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            DataProviders.Current.Products.Add(product);
            return Redirect("ViewAllProduct");
        }

        [HttpGet]
        public ActionResult ProductImportXLS()
        {
            var xlArray = DataProviders.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");
            ViewBag.xlArray = xlArray;
            ViewBag.xlArrayRows = xlArray.GetLength(0);
            ViewBag.xlArrayCols = xlArray.GetLength(1);
            return View();
        }
        [HttpPost]
        public ActionResult ProductImportXLS(string temp)
        {
            //для теста добавления в базу, по идее нужно с представления возвращать данные для добавления
            var xlArray = DataProviders.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");
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
            return View("Index");
        }
    }
}