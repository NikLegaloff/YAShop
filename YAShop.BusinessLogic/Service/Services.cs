using YAShop.BusinessLogic.Service.Carts;
using YAShop.BusinessLogic.Service.Categorys;
using YAShop.BusinessLogic.Service.Orders;
using YAShop.BusinessLogic.Service.Products;
using YAShop.BusinessLogic.Service.Users;

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