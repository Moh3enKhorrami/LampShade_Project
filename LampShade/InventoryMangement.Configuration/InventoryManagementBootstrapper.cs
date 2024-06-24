using InventoryManagement.Application;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
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
        
        services.AddDbContext<InventoryContext>(x => x.UseSqlServer(connectionString));
    }
}