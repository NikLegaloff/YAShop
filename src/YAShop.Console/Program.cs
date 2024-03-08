using System.Reflection;
using Csv;
using Newtonsoft.Json;
using YAShop.Common;
using YAShop.Common.Domain;
using YAShop.Common.Service.Cart;
using YAShop.Console;
using static System.Net.Mime.MediaTypeNames;

var paths = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) .Split('\\');
var path = string.Join("\\",paths.Take(paths.Length-4)) + "\\YAShop.Web.Storefront\\wwwroot\\data\\";
Registry.Init(new StaticCommonInfrProvider(),path);

foreach (var p in LoadCsvProducts(path))
{
    Registry.Current.Products.Save(p);
}


Product[] LoadCsvProducts(string path)
{

    var all = new List<Product>();
    var text = File.ReadAllText(path + "Inventory.csv");
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