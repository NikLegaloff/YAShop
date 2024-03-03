using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YAShop.Common.Data;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var prs = new List<ProductSummary>();
            var prs2 = new List<ProductSummary>();
            foreach (var p in Registry.Current.Products.SelectAll().Take(4))
            {
                prs.Add(new ProductSummary
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.Image,
                    Price = p.Price,
                    QTY = p.QTY
                });
            }
            foreach (var p in Registry.Current.Products.SelectAll().Skip(10).Take(4))
            {
                prs2.Add(new ProductSummary
                {
                    Id = p.Id,
                    Title = p.Title,
                    Image = p.Image,
                    Price = p.Price,
                    QTY = p.QTY
                });
            }
            return View(new IndexModel{TopProducts = prs.ToArray(),LatestProducts = prs2.ToArray()});
        }
    }
}
