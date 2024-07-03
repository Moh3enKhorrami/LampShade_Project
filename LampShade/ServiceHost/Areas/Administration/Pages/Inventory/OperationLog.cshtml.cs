using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.OperationLog
{
	public class OperationLogModel : PageModel
    {
        public List<InventoryOperationViewModel> Command;
        private readonly IInventoryApplication _inventoryApplication;
        public OperationLogModel(IProductApplication productApplication, IInventoryApplication inventoryApplication)
        {
            _inventoryApplication = inventoryApplication;
            Command = new List<InventoryOperationViewModel>();
        }
        
        public void OnGet(long id)
        {
               Command = _inventoryApplication.GetOperationLog(id);
        }
        
    }
}
