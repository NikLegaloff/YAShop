using System;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Domain.Model;
using Sprut.MyShop.Infrastructure.Providers;

namespace Sprut.MyShop.Infrastructure
{
    public class Registry
    {
        private static Registry _current;
        public CartDataProvider Carts;
        public CategoryDataProvider Categories;
        public OrderDataProvider Orders;

        public ProductDataProvider Products;

        // replace InMemoryDataProviderExecutor to SqlDataProviderExecutor after implementation
        //public IDataProvider<T> GetExecutor<T>() where T : DomainObject => new InMemoryDataProviderExecutor<T>();
        //public IDataProvider<T> GetExecutor<T>() where T : DomainObject => new MongoDataProviderExecutor<T>().Init();


        public Registry(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            CommonInfrastructureProvider = commonInfrastructureProvider;
            //Products=new ProductDataProvider(GetExecutor<Product>());
            Products = new ProductDataProvider(new EfDataProviderExecutor<Product>().Init());
            Carts = new CartDataProvider();
            Orders = new OrderDataProvider(new EfDataProviderExecutor<Order>().Init());
            Categories = new CategoryDataProvider(new EfDataProviderExecutor<Category>().Init());
        }

        public ICommonInfrastructureProvider CommonInfrastructureProvider { get; }

        public static Registry Current
        {
            get
            {
                if (_current == null) throw new Exception("Registry is not initialized");
                return _current;
            }
        }

        public static void Init(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            _current = new Registry(commonInfrastructureProvider);
        }
    }
}