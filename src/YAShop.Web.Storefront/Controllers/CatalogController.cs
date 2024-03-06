using Microsoft.AspNetCore.Mvc;
using YAShop.Common;
using YAShop.Common.Domain;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers;

public class YASController : Controller
{
    public ProductSummary[] SelectproductSummary(Product[] products)
    {
        var prs = new List<ProductSummary>();
        foreach (var p in products)
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
        return prs.ToArray();

    }
}
public class CatalogController : YASController
{
    public IActionResult View(Guid id)
    {
        return View(new ProductViewModel{Product = Registry.Current.Products.Find(id) ,SimilarProducts = SelectproductSummary(Registry.Current.Products.SelectAll().Skip(12).Take(4).ToArray()) });
    }
    public IActionResult Index()
    {
        return View(new CatalogModel() { Products = SelectproductSummary(Registry.Current.Products.SelectAll()) });
    }

}