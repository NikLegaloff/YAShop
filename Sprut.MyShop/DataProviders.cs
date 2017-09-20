namespace Sprut.MyShop
{
    public class DataProviders
    {
        private static DataProviders _current;
        public static DataProviders Current => _current ?? (_current = new DataProviders());

        public DataProviders()
        {
            Products = new TestProductProvider();
        }

        public IProductProvider Products { get; private set; }

    }

    public class CartProviders
    {
        private static CartProviders _current;
        public static CartProviders Current => _current ?? (_current = new CartProviders());

        public CartProviders()
        {
            Cart = new CartProvider();
        }

        public ICartProvider Cart { get; set; }

    }

    public class OrderProviders
    {
        private static OrderProviders _current;
        public static OrderProviders Current => _current ?? (_current = new OrderProviders());

        public OrderProviders()
        {
            Order = new OrderProvider();
        }

        public IOrderProvider Order { get; set; }

    }

    public class CategoryProviders
    {
        private static CategoryProviders _current;
        public static CategoryProviders Current => _current ?? (_current = new CategoryProviders());

        public CategoryProviders()
        {
            Category = new CategoryProvider();
        }

        public ICategoryProvider Category { get; set; }

    }
}