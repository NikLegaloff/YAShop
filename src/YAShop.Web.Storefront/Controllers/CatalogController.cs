using Microsoft.AspNetCore.Mvc;
using YAShop.Common.Data;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers;

public class CatalogController : Controller
{
    public IActionResult View(Guid id)
    {
        return View(Registry.Current.Products.SelectAll().FirstOrDefault(p => p.Id == id));
    }
    public IActionResult Index()
    {
        var prs = new List<ProductSummary>();
        foreach (var p in Registry.Current.Products.SelectAll())
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
        return View(new IndexModel { TopProducts = prs.ToArray() });
    }

}