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

        public IActionResult ViewProductEdit()
        {
            ViewBag.product = DataProviders.Current.Products.GetAll();
            return View(DataProviders.Current.Products.GetAll());
        }

        public ActionResult EditProduct(string sku)
        {
            return View();
        }


    }
}
