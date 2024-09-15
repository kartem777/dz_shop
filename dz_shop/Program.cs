using Microsoft.EntityFrameworkCore;
using dz_shop.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

var adminRole = new Role("admin");
var userRole = new Role("user");

var people = new List<User>
{
    new User("artem@gmail.com", "12345", adminRole),
    new User("test@gmail.com", "55555", userRole)
};

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
        options.AccessDeniedPath = "/accessdenied";
    });
builder.Services.AddAuthorization();


string connection = "Server = (localdb)\\mssqllocaldb;Database = myshoptdb;Trusted_Connection=true";
builder.Services.AddDbContext<ItemsContext>(options => options.UseSqlServer(connection));
builder.Services.AddControllersWithViews();
var app = builder.Build();


app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/login", async (HttpContext context) =>
{
    context.Response.ContentType = "text/html; charset=utf";
    await context.Response.SendFileAsync("login.html");
});


app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
{
    var form = context.Request.Form;
    if (!form.ContainsKey("email") || !form.ContainsKey("password"))
        return Results.BadRequest("Email or password not set");

    string email = form["email"];
    string password = form["password"];

    User? person = people.FirstOrDefault(p => p.Email == email && p.Password == password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim>
    {
        new Claim(ClaimsIdentity.DefaultNameClaimType, person.Email),
        new Claim(ClaimsIdentity.DefaultRoleClaimType, person.Role.Name)
    };
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
    return Results.Redirect(returnUrl ?? "/");
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapGet("/accessdenied", async (HttpContext context) =>
{
    context.Response.StatusCode = 403;
    await context.Response.WriteAsync("Access Denied");
});

app.MapDefaultControllerRoute();
app.Run();
