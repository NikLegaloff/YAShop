using Microsoft.AspNetCore.Mvc;
using YAShop.Common;

namespace YAShop.Web.Storefront.Controllers
{
    public class CustomPagesController : Controller
    {
        public IActionResult View(string alias)
        {
            var page = Registry.Current.CustomPages.SelectAll().FirstOrDefault(p=>p.Alias== alias);
            return View(page);
        }
    }
}
