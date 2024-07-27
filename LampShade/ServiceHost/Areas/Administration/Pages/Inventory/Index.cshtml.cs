using _0_Framework.Infrastructure;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using InventoryManagement.Application.Contract.Inventory;
using InventoryMangement.Configuration.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        public InventorySearchmodel SearchModel;
        public List<InventoryViewModel> Inventory; 
        public SelectList Products;
        
        private readonly IProductApplication _productApplication;
        private readonly IInventoryApplication _inventoryApplication;
        public IndexModel(IProductApplication productApplication, IInventoryApplication inventoryApplication )
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
            SearchModel = new InventorySearchmodel();
            Inventory = new List<InventoryViewModel>();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
        
        [NeedsPermission(InventoryPermission.ListInventory)]
        public void OnGet(InventorySearchmodel searchModel)
        {
            Inventory = _inventoryApplication.Search(searchModel);
        }
        
    }
       
        
}
