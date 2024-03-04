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

    public ICommonInfrastructureProvider Infrastructure { get; }
    public CartService Cart => new CartService();

    public Registry(ICommonInfrastructureProvider commonInfrastructureProvider, string? invFilePath = null)
    {
        Infrastructure = commonInfrastructureProvider;
        Products = new ProductDataProvider(invFilePath);
    }
}