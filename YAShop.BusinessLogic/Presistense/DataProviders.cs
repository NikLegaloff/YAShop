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
            Categories = GetDataProvider<Category>();
            Commands = GetDataProvider<CommandDTO>();
            Users = GetDataProvider<User>();
            InitData();
        }

        public IDataProvider<Order> Orders { get; private set; }
        public IDataProvider<Product> Products { get; private set; }
        public IDataProvider<Category> Categories { get; private set; }
        public IDataProvider<CommandDTO> Commands { get; private set; }
        public IDataProvider<User> Users { get; private set; }

        private void InitData()
        {
        }

        private IDataProvider<T> GetDataProvider<T>() where T : DomainObject, new()
        {
            return new MSSqlDataProvider<T>().Init();
        }
    }
}