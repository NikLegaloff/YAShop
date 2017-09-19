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

        public ActionResult ProductImportXLS()
        {
            var xlArray = DataProviders.Current.Products.ImportFromExcel("d:\\temp\\MyShopTest.xlsx");
            ViewBag.xlArray = xlArray;
            ViewBag.xlArrayRows = xlArray.GetLength(0);
            ViewBag.xlArrayCols = xlArray.GetLength(1);
            return View();
        }

    }
}