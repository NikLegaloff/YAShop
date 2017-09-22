using System;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure.Providers;

namespace Sprut.MyShop.Infrastructure
{
    public class Registry
    {
        public ICommonInfrastructureProvider CommonInfrastructureProvider { get; }

        private static Registry _current;
        public static Registry Current
        {
            get
            {
                if (_current==null) throw new Exception("Registry is not initialized");
                return _current;
            }
        }

        public static void Init(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            _current=new Registry(commonInfrastructureProvider);
        }

        // replace InMemoryDataProviderExecutor to SqlDataProviderExecutor after implementation
        public IDataProvider<T> GetExecutor<T>() where T : DomainObject => new InMemoryDataProviderExecutor<T>();

        public Registry(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            CommonInfrastructureProvider = commonInfrastructureProvider;
            Products=new ProductDataProvider(GetExecutor<Product>());
            Categories = new CategoryDataProvider(GetExecutor<Category>());
        }

        public ProductDataProvider Products;
        public CategoryDataProvider Categories;


    }

}