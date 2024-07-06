using System.Reflection;
using YAShop.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

var paths = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location).Split('\\');
var path = string.Join("\\", paths.Take(paths.Length - 4)) + "\\YAShop.Web.Storefront\\wwwroot\\data\\";
Registry.Init(new WebCommonInfrastructureProvider(), path);

app.Run();