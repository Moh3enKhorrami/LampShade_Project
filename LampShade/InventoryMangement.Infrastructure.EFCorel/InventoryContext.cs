using InventoryManagement.Domain.InventoryAgg;
using InventoryMangement.Infrastructure.EFCorel.Mapping;
using Microsoft.EntityFrameworkCore;

namespace InventoryMangement.Infrastructure.EFCorel;

public class InventoryContext : DbContext
{
    public DbSet<Inventory> Inventories { get; set; }
    public InventoryContext(DbContextOptions<InventoryContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var assembly = typeof(InventoryMapping).Assembly;
        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
        base.OnModelCreating(modelBuilder);
    }
}