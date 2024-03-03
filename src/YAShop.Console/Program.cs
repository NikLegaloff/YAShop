// See https://aka.ms/new-console-template for more information

using System.Net.Http.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using YAShop.Common.Data;

foreach (var p in Registry.Current.Products.SelectAll())
{
    Console.WriteLine(JsonConvert.SerializeObject(p,Formatting.Indented));
}