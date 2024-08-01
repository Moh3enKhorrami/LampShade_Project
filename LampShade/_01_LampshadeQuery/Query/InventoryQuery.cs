using _01_LampshadeQuery.Contracts.Inventory;
using InventoryMangement.Infrastructure.EFCorel;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;

namespace _01_LampshadeQuery.Query;

public class InventoryQuery : IInventoryQuery
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext;
    public InventoryQuery(ShopContext shopContext, InventoryContext inventoryContext)
    {
        _shopContext = shopContext;
        _inventoryContext = inventoryContext;
    }

    public StockStatus CheckStock(IsInStock command)
    {
        var inventory = _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == command.ProductId);
        if (inventory == null || inventory.CalculateCurrentCount() < command.Count)
        {
            var product = _shopContext.Products
                .Select(x => new { x.Id, x.Name })
                .FirstOrDefault(x => x.Id == command.ProductId);
            return new StockStatus
            {
                IsStock = false,
                ProductName = product.Name,
            };
        }

        return new StockStatus
        {
            IsStock = true,
        };
    }
}