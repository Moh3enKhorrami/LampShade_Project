using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryMangement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Inventory;

public class Increase : PageModel
{
    public IncreaseInventory Command;
    private readonly IInventoryApplication _inventoryApplication;
    public Increase(IInventoryApplication inventoryApplication)
    {
        _inventoryApplication = inventoryApplication;
    }
    [NeedsPermission(InventoryPermission.IncreaseInventory)]
    public void OnGet(long id)
    {
        Command = new IncreaseInventory()
        {
            InventoryId = id
        };
    }
    
    [NeedsPermission(InventoryPermission.IncreaseInventory)]
    public IActionResult OnPostIncrease(IncreaseInventory command)
    {
        var result = _inventoryApplication.Increase(command);
        return RedirectToPage("./index");
    }
    
}