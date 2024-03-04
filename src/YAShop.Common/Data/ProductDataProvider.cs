using Csv;
using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class ProductDataProvider
{
    private readonly string? _invFilePath;

    public ProductDataProvider(string? invFilePath=null)
    {
        _invFilePath = invFilePath;
    }

    public Product? Find(Guid id) => SelectAll().FirstOrDefault(p => p.Id == id);
    private Product[]? _products = null;

    public Product[] SelectAll()
    {
        return _products??= string.IsNullOrEmpty(_invFilePath) ? DummyProducts() : CsvProducts() ;
        
    }

    private Product[] CsvProducts()
    {
        var all = new List<Product>();
        
        foreach (var csv in CsvReader.ReadFromText(File.ReadAllText(_invFilePath)))
        {
            all.Add(new Product
            {
                Id = csv["Id"].ToGuid(),
                SKU = csv["SKU"],
                Title= csv["Title"],
                Image= csv["Image1"],
                Price= csv["BIN price"].ToDecimal(),
                QTY = csv["QTY"].ToInt(),
                Description= csv["Description"],
            });
        }
        return all.ToArray();
    }

    private static Product[] DummyProducts()
    {
        var all = new List<Product>();
        for (int i = 10; i <= 30; i++)
            all.Add(new Product
            {
                Id = Guid.Parse("e4aaf240-8f6a-4485-b040-594c579425" + i), 
                SKU = "P-" + i, 
                Title = $"Product #{i} title",
                Price = 10+i*2.5m, 
                QTY = i-1,
                Image = "https://upload.wikimedia.org/wikipedia/commons/0/0e/Canon_EOS_50D.jpg",
                Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum."
            });
            
        return all.ToArray();
    }
}