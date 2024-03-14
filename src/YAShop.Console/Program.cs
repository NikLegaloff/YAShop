using System.IO;
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

var cts = Registry.Current.StoreCategories.SelectAll();
var i = 0;
foreach(var p in Registry.Current.Products.SelectAll())
{
    p.StoreCategoryId = cts[i++ % 3].Id;
    Registry.Current.Products.Save(p);
}
/*var Categories = new[] { "Tools", "Parts", "Others" };
foreach(var category in Categories)
{
    var sc = new StoreCategory();
    sc.Name = category;
    Registry.Current.StoreCategories.Save(sc);
  
}*/