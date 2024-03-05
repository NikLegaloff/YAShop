using Csv;
using YAShop.Common.Domain;

namespace YAShop.Common.Data;

public class ProductDataProvider : IDataProvider<Product>
{
    private readonly string? _dataPath;

    public ProductDataProvider(string? dataPath=null)
    {
        _dataPath = dataPath;
    }


    public Product[] SelectAll()
    {
        return _products ??=LoadCsvProducts();
    }

    public Product? Find(Guid id)
    {
        return SelectAll().FirstOrDefault(p => p.Id == id);
    }

    public void Save(Product subj)
    {
        throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
        throw new NotImplementedException();
    }


    private Product[]? _products;
    Product[] LoadCsvProducts()
    {

        var all = new List<Product>();
        var text = File.ReadAllText(_dataPath + "Inventory.csv");
        foreach (var csv in CsvReader.ReadFromText(text))
        {
            var item = new Product();
            item.Id = csv["Id"].ToGuid();
            item.SKU = csv["SKU"];
            item.Title = csv["Title"];
            item.Image = csv["Image1"];
            item.Price = csv["BIN price"].ToDecimal();
            item.QTY = csv["QTY"].ToInt();
            item.Description = csv["Description"];
            all.Add(item);
        }
        return all.ToArray();
    }
}