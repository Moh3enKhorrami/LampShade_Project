using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryManagement.Domain.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class EditModel : PageModel
    {
        public EditInventory Command;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;
        public SelectList Products;
        public EditModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
            Command = new EditInventory();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public void OnGet(long id)
        {
            Command = _inventoryApplication.Getdetails(id);
        }

        public IActionResult OnPostEdit(EditInventory Command)
        {
            var result = new OperationResult();
            result = _inventoryApplication.Edit(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                
            }
            
        }
        
        
       
    }
}