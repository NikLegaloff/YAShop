using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sprut.MyShop;

namespace WebAppAdmin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult ViewProduct()
        {
            ViewBag.product = DataProviders.Current.Products.GetAll();
            return View();
        }

        public ActionResult ViewProductEdit()
        {
            ViewBag.product = DataProviders.Current.Products.GetAll();
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

            return Redirect("ViewProductEdit");
        }


        public IActionResult ProductImport()
        {
            var xlArray = DataProviders.Current.Products.ImportFromExcel("e:\\temp\\MyShopTest.xlsx");
            ViewBag.xlArray = xlArray;
            return View();
        }

    }
}
