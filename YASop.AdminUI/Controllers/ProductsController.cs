using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Service.Product;
using YAShop.BusinessLogic.Service.Product.Viewing;

namespace YASop.AdminUI.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Select(ProductFilter filter)
        {
            return Json(await Registry.Current.Services.Product.SelectPage(filter), JsonRequestBehavior.AllowGet);
        }
    }

    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}