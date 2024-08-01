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
// Start Policy Authorization for Access
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminArea", builder =>
        builder.RequireRole(new List<string> { Roles.Administrator, Roles.Creator }));
    options.AddPolicy("Shop", builder =>
        builder.RequireRole(new List<string> { Roles.Administrator, Roles.Creator }));
    options.AddPolicy("Account", builder =>
        builder.RequireRole(new List<string> { Roles.Administrator }));
    options.AddPolicy("Discount", builder =>
        builder.RequireRole(new List<string> { Roles.Administrator }));
});

// builder.Services.AddCors(options =>
//     options.AddPolicy("MyPolicy", builder => builder.WithOrigins().WithHeaders().WithMethods())); // CORS API

builder.Services.AddRazorPages()
    .AddMvcOptions(options => options.Filters.Add<SecurityPageFilter>())
    .AddRazorPagesOptions(options =>
    {
        options.Conventions.AuthorizeAreaFolder("Administration", "/", "AdminArea");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Shop", "Shop");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Accounts", "Account");
        options.Conventions.AuthorizeAreaFolder("Administration", "/Discounts", "Discount");
    })
    .AddNewtonsoftJson();
    // .AddApplicationPart(typeof(ProductController).Assembly);
    // .AddApplicationPart(typeof(InventoryController).Assembly);


// End Policy Authorization for Access
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseAuthentication(); // Add
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCookiePolicy(); // Add
app.UseRouting();

app.UseAuthorization(); // Add
// app.UseCors("MyPolicy"); // CORS API
app.UseEndpoints(endpoint =>
{
    app.MapRazorPages();
    app.MapControllers();
});


app.Run();
