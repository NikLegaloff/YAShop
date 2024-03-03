using Microsoft.AspNetCore.Mvc;
using YAShop.Common;

namespace YAShop.Web.Storefront.Controllers;

public class CartController : Controller
{
    [HttpPost]
    public IActionResult Add(Guid id, int qty)
    {
        Registry.Current.Cart.Add(id, qty);
        return Redirect(Request.Headers["Referer"].ToString());
    }

}