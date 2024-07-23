using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
	public class CreateModel : PageModel
    {
        public CreateProductCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;
        
        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            Command = new CreateProductCategory();
        }
        
        public void OnGet()
        {
        }

        public IActionResult OnPostCreate(CreateProductCategory Command)
        {
            var result = _productCategoryApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return Page();
            }
        }
    }
}
