using YAShop.Common.Domain;
using YAShop.Common.Service.Cart;

namespace YAShop.Web.Storefront.Models
{
    public class CheckoutModel
    {
        public Cart Cart { get; set; }
        public City[] Cities { get; set; }

    }
}
