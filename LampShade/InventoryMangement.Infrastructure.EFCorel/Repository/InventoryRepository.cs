using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;

namespace InventoryMangement.Infrastructure.EFCorel.Repository;

public class InventoryRepository : RepositoryBase<long, Inventory> , IInventoryRepository
{
    private readonly ShopContext _shopContext;
    private readonly InventoryContext _inventoryContext; 
    public InventoryRepository(InventoryContext inventoryContext, ShopContext shopContext) : base(inventoryContext)
    {
        _inventoryContext = inventoryContext;
        _shopContext = shopContext;
    }

    public EditInventory GetDetails(long id)
    {
        return _inventoryContext.Inventories.Select(x => new EditInventory()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice
        }).FirstOrDefault(x => x.Id == id);
    }

    public Inventory GetBy(long productId)
    {
        return _inventoryContext.Inventories.FirstOrDefault(x => x.ProductId == productId);
    }

    public List<InventoryViewModel> Search(InventorySearchmodel searchModel)
    {
        var products = _shopContext.Products.Select(x => new { x.Id, x.Name }).ToList();
        var query = _inventoryContext.Inventories.Select(x => new InventoryViewModel()
        {
            Id = x.Id,
            ProductId = x.ProductId,
            UnitPrice = x.UnitPrice,
            InStock = x.InStock,
            CurrentCount = x.CalculateCurrentCount()
        });
        if (searchModel.ProductId > 0)
            query = query.Where(x => x.ProductId == searchModel.ProductId);
        
        if (!searchModel.InStock)
            query = query.Where(x => !x.InStock);
        var inventory = query.OrderByDescending(x => x.Id).ToList();

        inventory.ForEach(item 
            => item.Product = products.FirstOrDefault(x => x.Id == item.ProductId)?.Name);

        return inventory;

    }
}