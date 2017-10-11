using System.Threading.Tasks;
using System.Web.Mvc;
using YAShop.BusinessLogic;
using YAShop.BusinessLogic.Service.Products.Viewing;

namespace YASop.AdminUI.Controllers
{
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