using Microsoft.AspNetCore.Mvc;
using YAShop.Common.Domain;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers;

public class YASControllerBase : Controller
{
    public ProductSummary[] SelectProductSummary(Product[] products)
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