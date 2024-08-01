using _0_Framework.Infrastructure;
using _01_LampshadeQuery.Contracts.Inventory;
using _01_LampshadeQuery.Query;
using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using InventoryMangement.Configuration.Permissions;
using InventoryMangement.Infrastructure.EFCorel;
using InventoryMangement.Infrastructure.EFCorel.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryMangement.Configuration;

public class InventoryManagementBootstrapper
{
    public static void Configure(IServiceCollection services, string connectionString)
    {
        services.AddTransient<IInventoryRepository, InventoryRepository>();
        services.AddTransient<IInventoryApplication, InventoryApplication>();
        services.AddTransient<IPermissionExposer, InventoryPermissionExposer>();
        services.AddTransient<IInventoryQuery, InventoryQuery>();
        services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}