using _0_Framework.Application;
using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts;
using AccountManagement.Configuration;
using CommentManagement.Configuration;
using DiscountManagement.Configuration;
using InventoryMangement.Configuration;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddHttpContextAccessor(); // AuthHelper

var connectionString = builder.Configuration.GetConnectionString("LampshadeDb");

ShopManagementBoostrapper.Configure(builder.Services, connectionString);
DiscountManagementBoostrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBoostrapper.Configure(builder.Services, connectionString);
AccountManagementBootStrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUpLoader, FileUpLoader>();
builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IAuthHelper, AuthHelper>();
// start CookiePolicyOptions
builder.Services.Configure<CookiePolicyOptions>(options =>
{
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Lax;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
    {
        o.LoginPath = new PathString("/Account");
        o.LogoutPath = new PathString("/Account");
        o.AccessDeniedPath = new PathString("/AccessDenied");
    });
// End CookiePolicyOptions
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminArea", builder =>
            builder.RequireRole(new List<string> { Roles.Administrator, Roles.Creator }));
        options.AddPolicy("Shop", builder =>
            builder.RequireRole(new List<string> { Roles.Administrator}));
        options.AddPolicy("Inventory", builder =>
            builder.RequireRole(new List<string> { Roles.Administrator}));
        options.AddPolicy("Account", builder =>
            builder.RequireRole(new List<string> { Roles.Administrator}));
        options.AddPolicy("Discount", builder =>
            builder.RequireRole(new List<string> { Roles.Administrator}));
    });
    
builder.Services.AddRazorPages()
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Inventory", "Inventory");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");

    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//app.MapDefaultControllerRoute();

app.Run();

