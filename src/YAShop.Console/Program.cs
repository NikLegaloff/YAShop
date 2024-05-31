using System.Reflection;
using YAShop.Common;
using YAShop.Console;

var paths = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split('\\');
var path = string.Join("\\", paths.Take(paths.Length - 4)) + "\\YAShop.Web.Storefront\\wwwroot\\data\\";
Registry.Init(new StaticCommonInfrProvider(), path);