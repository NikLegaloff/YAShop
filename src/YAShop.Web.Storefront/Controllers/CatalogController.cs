using Microsoft.AspNetCore.Mvc;
using YAShop.Common;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers;

public class CatalogController : YASControllerBase
{
    public IActionResult View(Guid id)
    {
        return View(new ProductViewModel{Product = Registry.Current.Products.Find(id) ,SimilarProducts = SelectProductSummary(Registry.Current.Products.SelectAll().Skip(12).Take(4).ToArray()) });
    }
    public IActionResult Index(string? query=null, Guid? categoryId=null)
    {
        var all = Registry.Current.Products.SelectAll().ToArray();
        if (!string.IsNullOrEmpty(query)) all = all.Where(p => p.Title.Contains(query, StringComparison.OrdinalIgnoreCase)).ToArray();
        if (categoryId != null) all = all.Where(p => p.StoreCategoryId == categoryId.Value).ToArray();
        
        return View(new CatalogModel() { Products = SelectProductSummary(all) });
    }

}