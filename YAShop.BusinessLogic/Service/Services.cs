using YAShop.BusinessLogic.Service.Cart;
using YAShop.BusinessLogic.Service.Category;
using YAShop.BusinessLogic.Service.Order;
using YAShop.BusinessLogic.Service.Product;
using YAShop.BusinessLogic.Service.User;

namespace YAShop.BusinessLogic.Service
{
    public class Services
    {
        public UserService User { get; private set; }
        public CartService Cart { get; private set; }
        public OrderService Order { get; private set; }
        public ProductService Product { get; private set; }
        public CategoryService Category { get; private set; }

        internal static Services Create()
        {
            return new Services
            {
                User = new UserService(),
                Cart = new CartService(),
                Order = new OrderService(),
                Product = new ProductService(),
                Category = new CategoryService()
            };
        }
    }
}