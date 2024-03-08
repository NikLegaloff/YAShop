using Csv;
using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class ProductDataProvider : FileJsonDataProvider<Product>
{
    public ProductDataProvider(string? dataPath = null) : base(dataPath)
    {
    }
}