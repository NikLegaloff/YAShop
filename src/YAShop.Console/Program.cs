using Newtonsoft.Json;
using YAShop.Common;
using YAShop.Console;

Registry.Init(new StaticCommonInfrProvider());

foreach (var p in Registry.Current.Products.SelectAll())
{
    Console.WriteLine(JsonConvert.SerializeObject(p,Formatting.Indented));
}