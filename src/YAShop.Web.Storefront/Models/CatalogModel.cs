using System.Dynamic;
using YAShop.Common.Service.Cart;

namespace YAShop.Web.Storefront.Models;

public class CatalogModel
{
    public ProductSummary[] Products { get; set; }
    public CartItem CartItem { get; set; }
    
}