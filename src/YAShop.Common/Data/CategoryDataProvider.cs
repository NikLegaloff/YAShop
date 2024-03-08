using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class CategoryDataProvider : FileJsonDataProvider<Category>{
    public CategoryDataProvider(string? dataPath = null) : base(dataPath)
    {
    }
}