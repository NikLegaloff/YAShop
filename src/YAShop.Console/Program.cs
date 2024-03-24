using System.IO;
using System.Reflection;
using Csv;
using Newtonsoft.Json;
using YAShop.Common;
using YAShop.Common.Domain;
using YAShop.Common.Service.Cart;
using YAShop.Console;
using static System.Net.Mime.MediaTypeNames;

var paths = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split('\\');
var path = string.Join("\\", paths.Take(paths.Length - 4)) + "\\YAShop.Web.Storefront\\wwwroot\\data\\";
Registry.Init(new StaticCommonInfrProvider(), path);

