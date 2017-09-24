using YAShop.BusinessLogic.DomainModel;
using YAShop.BusinessLogic.Presistense.MSSQL;

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

        }

        private IDataProvider<T> GetDataProvider<T>() where T : DomainObject, new()
        {
            return new MSSqlDataProvider<T>().Init();
        }

        public IDataProvider<Order> Orders { get; private set; }
        public IDataProvider<Product> Products { get; private set; }
    }
}