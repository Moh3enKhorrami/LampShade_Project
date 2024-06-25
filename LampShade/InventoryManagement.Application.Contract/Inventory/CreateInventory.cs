using System.ComponentModel.DataAnnotations;
using ShopManagement.Application.Contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory;

public class CreateInventory
{
    [Range(1,1000000)]
    public long ProductId { get; set; }
    
    [Range(1, double.MaxValue)]
    public double UnitPrice { get; set; }
    
    public List<ProductViewModel> Products { get; set; }
    
}