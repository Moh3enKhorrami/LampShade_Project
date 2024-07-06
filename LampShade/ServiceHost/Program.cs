﻿using _0_Framework.Application;
using DiscountManagement.Configuration;
using InventoryMangement.Configuration;
using Microsoft.Extensions.Configuration;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("LampshadeDb");

ShopManagementBoostrapper.Configure(builder.Services, connectionString);
DiscountManagementBoostrapper.Configure(builder.Services, connectionString);
InventoryManagementBootstrapper.Configure(builder.Services, connectionString);

builder.Services.AddTransient<IFileUpLoader, FileUpLoader > ();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
//app.MapDefaultControllerRoute();

app.Run();

