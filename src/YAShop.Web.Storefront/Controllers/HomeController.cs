using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YAShop.Common;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers
{
    public class HomeController : YASControllerBase
    {
        public IActionResult Index()
        {
            var topProducts = SelectProductSummary(Registry.Current.Products.SelectAll().Take(4).ToArray());
            var latestProducts = SelectProductSummary(Registry.Current.Products.SelectAll().Skip(10).Take(4).ToArray());
            return View(new IndexModel{TopProducts = topProducts, LatestProducts = latestProducts });
        }
       
    }

}
