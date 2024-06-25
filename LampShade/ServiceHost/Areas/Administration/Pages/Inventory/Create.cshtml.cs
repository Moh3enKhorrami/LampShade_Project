using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
	public class CreateModel : PageModel
    {
        public CreateInventory Command;
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;
        public SelectList Products;
        public CreateModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPostCreate(CreateInventory Command)
        {
            var result = _inventoryApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
