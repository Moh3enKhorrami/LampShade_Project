using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryMangement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
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
        
        [NeedsPermission(InventoryPermission.OperationLog)]
        public void OnGet(long id)
        {
               Command = _inventoryApplication.GetOperationLog(id);
        }
        
    }
}
