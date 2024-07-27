using _0_Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Configuration.Permissions;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
	public class CreateModel : PageModel
    {
        public CreateProduct Command;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList ProductCategories;
        public CreateModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            _productApplication = productApplication;
            //Command = new CreateProduct();
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(),"Id","Name");
        }
        
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public void OnGet()
        {
            
        }
        
        [NeedsPermission(ShopPermissions.CreateProduct)]
        public IActionResult OnPostCreate(CreateProduct Command)
        {
            //ProductCategories.GetEnumerator();
            var result = _productApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
