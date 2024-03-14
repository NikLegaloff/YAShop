using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class StoreCategoryDataProvider : FileJsonDataProvider<StoreCategory>{
    public StoreCategoryDataProvider(string? dataPath = null) : base(dataPath)
    {
    }
}