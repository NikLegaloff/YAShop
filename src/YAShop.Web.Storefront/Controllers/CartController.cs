using Microsoft.AspNetCore.Mvc;

namespace YAShop.Web.Storefront.Controllers;

public class CartController : Controller
{
    [HttpPost]
    public IActionResult Add(Guid id, int qty)
    {
        return Redirect(Request.Headers["Referer"].ToString());
    }

}