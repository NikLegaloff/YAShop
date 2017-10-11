using System.Web.Mvc;
using YAShop.BusinessLogic;
using YASop.AdminUI.Code;

namespace YASop.AdminUI.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogOff()
        {
            new AuthHelper().LogOff();
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Login(string login, string pass)
        {
            Registry.Current.Services.User.Get(login, pass);
            return View();
        }

    }
}