using YAShop.Common;
using YAShop.Web.Storefront.Code;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(300);
    options.Cookie.HttpOnly = false;
    options.Cookie.IsEssential = true;
    options.Cookie.Name = ".YAShop.Session";
});


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
//app.UseAuthorization();
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSession();

var path = app.Environment.WebRootPath + "\\data\\";
WebCommonInfrastructureProvider.App = app;
Registry.Init(new WebCommonInfrastructureProvider(), path);

app.Run();

