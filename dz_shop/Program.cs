using Microsoft.EntityFrameworkCore;
using dz_shop.Models;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server = (localdb)\\mssqllocaldb;Database = myshopdb;Trusted_Connection=true";
builder.Services.AddDbContext<ItemsContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
var app = builder.Build();
//app.UseHttpsRedirection();
//app.MapGet("/", () => "Hello");
app.MapDefaultControllerRoute();
app.Run();
