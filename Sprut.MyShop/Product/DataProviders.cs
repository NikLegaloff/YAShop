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
}