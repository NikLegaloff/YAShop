using YAShop.BusinessLogic.DomainModel;

namespace YAShop.BusinessLogic.Presistense
{
    public class DataProviders
    {
        public DataProviders()
        {
            Orders = GetDataProvider<Order>();
            Products = GetDataProvider<Product>();
            InitData();
        }

        private void InitData()
        {
            Products.Save(new Product { SKU = "M1", Title = "Canon 40D", Price = 200, QTY = 2 });
            Products.Save(new Product { SKU = "M2", Title = "Canon EF-50/1.8", Price = 100, QTY = 3 });

        }

        private IDataProvider<T> GetDataProvider<T>() where T : DomainObject, new()
        {
            return new IMDBDataProvider<T>();
        }

        public IDataProvider<Order> Orders { get; private set; }
        public IDataProvider<Product> Products { get; private set; }
    }
}