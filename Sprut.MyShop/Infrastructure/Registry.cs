using System;
using Sprut.MyShop.Domain;
using Sprut.MyShop.Infrastructure.Providers;
using Sprut.MyShop.Infrastructure;


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
        //public IDataProvider<T> GetExecutor<T>() where T : DomainObject => new InMemoryDataProviderExecutor<T>();
        //public IDataProvider<T> GetExecutor<T>() where T : DomainObject => new MongoDataProviderExecutor<T>().Init();


        public Registry(ICommonInfrastructureProvider commonInfrastructureProvider)
        {
            CommonInfrastructureProvider = commonInfrastructureProvider;
            Products = new ProductDataProvider(new EfDataProviderExecutor<Product>().Init());
            
            Carts = new CartDataProvider();
            Categories = new CategoryDataProvider(GetExecutor<Category>());
        }

        public ProductDataProvider Products;
<<<<<<< HEAD
        public CartDataProvider Cart;
=======
        public CartDataProvider Carts;
>>>>>>> 7fdb7a056ddb9b7fbfa32cd8140d71f80837dd82
        public CategoryDataProvider Categories;
        


    }

}