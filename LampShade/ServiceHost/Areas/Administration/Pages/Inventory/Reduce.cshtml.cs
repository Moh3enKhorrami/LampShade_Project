using _0_Framework.Infrastructure;
using InventoryManagement.Application.Contract.Inventory;
using InventoryMangement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Areas.Administration.Pages.Inventory;

public class Reduce : PageModel
{
    public ReduceInventory Command;
    private readonly IInventoryApplication _inventoryApplication;

    public Reduce(IInventoryApplication inventoryApplication)
    {
        _inventoryApplication = inventoryApplication;
    }
    public void OnGet(long id)
    {
        Command = new ReduceInventory()
        {
            InventoryId = id
        };
    }
    
    [NeedsPermission(InventoryPermission.ReduceInventory)]
    public IActionResult OnPostReduce(ReduceInventory command)
    {
        var result = _inventoryApplication.Reduce(command);
        return RedirectToPage("./index");
    }
}