using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YAShop.Common;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers
{
    public class HomeController : YASController
    {
        public IActionResult Index()
        {
            var topProducts = SelectproductSummary(Registry.Current.Products.SelectAll().Take(4).ToArray());
            var latestProducts = SelectproductSummary(Registry.Current.Products.SelectAll().Skip(10).Take(4).ToArray());
            return View(new IndexModel{TopProducts = topProducts, LatestProducts = latestProducts });
        }
    }
}
