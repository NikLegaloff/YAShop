using System.Dynamic;
using System.Threading.Tasks;
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
            return Redirect("/");
        }
        [AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Login()
        {
            dynamic res = new ExpandoObject();
            res.Error = "";
            return View(res);
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public async Task<ActionResult> Login(string email, string pass)
        {
            var user = await Registry.Current.Services.User.Get(email, pass);
            if (user != null)
            {
                new AuthHelper().Authenticate(user.Id);
                return Redirect("/");
            }
            dynamic res = new ExpandoObject();
            res.Error = "Invalid email/password";
            return View(res);
        }

    }
}