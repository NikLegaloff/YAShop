using System.Globalization;
using YAShop.Common.Data;
using YAShop.Common.Infrastructure;
using YAShop.Common.Service.Cart;

namespace YAShop.Common;

public class Registry
{
    private static Registry? _current;
    public static Registry Current => _current ?? throw new Exception("Registry is not initialized");

    public static void Init(ICommonInfrastructureProvider commonInfrastructureProvider, string? invFilePath=null)
    {
        _current = new Registry(commonInfrastructureProvider, invFilePath);
    }

    public ProductDataProvider Products { get; private set; }

    public CustomPageDataProvider CustomPages { get; private set; }

    public StoreCategoryDataProvider StoreCategories { get; private set; }
    public CityDataProvider Cities { get; private set; }
    public ICommonInfrastructureProvider Infrastructure { get; }
    public CartService Cart => new();

    public Registry(ICommonInfrastructureProvider commonInfrastructureProvider, string? dataPath = null)
    {
        CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
        Infrastructure = commonInfrastructureProvider;
        Products = new ProductDataProvider(dataPath);
        CustomPages = new CustomPageDataProvider(dataPath);
        StoreCategories = new StoreCategoryDataProvider(dataPath);
        Cities = new CityDataProvider(dataPath);
    }
}