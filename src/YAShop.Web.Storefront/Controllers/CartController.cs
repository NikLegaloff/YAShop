using Microsoft.AspNetCore.Mvc;
using YAShop.Common;
using YAShop.Common.Service.Cart;
using YAShop.Web.Storefront.Models;

namespace YAShop.Web.Storefront.Controllers;

public class CartController : Controller
{
    public IActionResult Add(Guid id, int qty)
    {
        Registry.Current.Cart.Add(id, qty);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    public IActionResult Index()
    {
        return View(Registry.Current.Cart.GetCart());
    }
    public IActionResult Checkout()
    {
        var cart = Registry.Current.Cart.GetCart();
        if (cart.IsEmpty) return Redirect("/");
        return View(new CheckoutModel {
            Cart = cart,
            Cities = Registry.Current.Cities. SelectAll()       
        });
    }
}