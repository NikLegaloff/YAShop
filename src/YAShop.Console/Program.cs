// See https://aka.ms/new-console-template for more information

using YAShop.Common.Data;

foreach (var p in Registry.Current.Products.SelectAll())
{
    Console.WriteLine(p.SKU + "\t" + p.Title + "\t" + p.Price);
}