using YAShop.Common.Domain;
using YAShop.Common.Service.Cart;

namespace YAShop.Web.Storefront.Models.Checkout
{
    public class CheckoutModel
    {
        public Cart Cart { get; set; }
        public City[] Cities { get; set; }
         
        public bool IsConfirm { get; set; }

    }
    public record CheckoutModel2(Cart Cart, City[] Cities, bool IsConfirm);
}
