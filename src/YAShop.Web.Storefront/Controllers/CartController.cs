using Microsoft.AspNetCore.Mvc;
using YAShop.Common;
using YAShop.Common.Domain;
using YAShop.Common.Domain.Checkout;
using YAShop.Common.Service.Cart;
using YAShop.Web.Storefront.Models;
using YAShop.Web.Storefront.Models.Checkout;

namespace YAShop.Web.Storefront.Controllers;

public class CartController : Controller
{
    public IActionResult Add(Guid id, int qty)
    {
        Registry.Current.CartService.Add(id, qty);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    public IActionResult Index()
    {
        return View(Registry.Current.CartService.GetCart());
    }


    [HttpGet]
    public IActionResult Checkout()
    {
        var cart = Registry.Current.CartService.GetCart();
        if (cart.IsEmpty) return Redirect("/");
        return View(new CheckoutModel
        {
            Cart = cart,
            Cities = Registry.Current.Cities.SelectAll()
        });
    }

    [HttpPost]
    public IActionResult Checkout(CheckoutDetails subj)
    {
        var cart = Registry.Current.CartService.GetCart();
        if (cart.IsEmpty) return Redirect("/");
        cart.CheckoutDetails = subj;
        Registry.Current.CartService.SaveCart(cart);
        return RedirectToAction("CheckoutConfirm");
    }


    [HttpGet]
    public IActionResult CheckoutConfirm()
    {
        var cart = Registry.Current.CartService.GetCart();
        if (cart.IsEmpty) return Redirect("/");
        return View(new CheckoutModel
        {
            Cart = cart,
            Cities = Registry.Current.Cities.SelectAll()
        });
    }

    [HttpGet]
    public IActionResult CheckoutCreate()
    {
        var cart = Registry.Current.CartService.GetCart();
        if (cart.IsEmpty) return Redirect("/");
        Registry.Current.OrderService.CreateOrder(cart);
        // ******
        cart.Items.Clear();
        if (cart.CheckoutDetails != null) cart.CheckoutDetails.Comment = null;
        Registry.Current.CartService.SaveCart(cart);

        return RedirectToAction("CheckoutComplete", new {orderId=Guid.Empty});
    }

    public IActionResult CheckoutComplete(Guid orderId)
    {
        return View(new CheckoutCompleteModel());
    }


}