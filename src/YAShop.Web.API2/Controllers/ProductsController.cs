using Microsoft.AspNetCore.Mvc;
using YAShop.Common;
using YAShop.Common.Domain;

namespace YAShop.Web.API2.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoriesController : ControllerBase
{
    [HttpGet(Name = "StoreCategoriesSelectAll")]
    public StoreCategory[] SelectAll()
    {
        return Registry.Current.StoreCategories.SelectAll();
    }
}

    [ApiController]
[Route("[controller]/[action]")]
public class ProductsController : ControllerBase
{

    [HttpGet(Name = "ProductsSelectAll")]
    public ProductSummary[] SelectAll()
    {
        var selectAll = Registry.Current.Products.SelectAll();
        return selectAll.Select(p=>new ProductSummary{Id = p.Id,SKU = p.SKU,Title = p.Title}).ToArray();
    }
    [HttpGet(Name = "ProductsFind")]
    public Product? Find(Guid id)
    {
        return Registry.Current.Products.Find(id);
    }
    [HttpGet(Name = "ProductsSave")]
    public Guid Save(Product product)
    {
        Registry.Current.Products.Save(product);
        return product.Id;
    }
}

public class ProductSummary
{
    public Guid Id { get; set; }
    public string SKU{ get; set; }
    public string Title{ get; set; }

}
