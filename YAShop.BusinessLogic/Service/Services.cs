namespace YAShop.BusinessLogic.Service
{
    public class Services
    {

        internal static Services Create()
        {
            return new Services { Cart = new CartService(), Order = new OrderService(), Product = new ProductService() };
        }

        public CartService Cart { get; private set; }
        public OrderService Order { get; private set; }
        public ProductService Product { get; private set; }
    }
}