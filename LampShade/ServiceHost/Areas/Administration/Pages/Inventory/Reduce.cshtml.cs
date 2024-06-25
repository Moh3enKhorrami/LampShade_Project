using InventoryManagement.Application.Contract.Inventory;
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
    
    public IActionResult OnPostReduce(ReduceInventory command)
    {
        var result = _inventoryApplication.Reduce(command);
        return RedirectToPage("./index");
    }
}