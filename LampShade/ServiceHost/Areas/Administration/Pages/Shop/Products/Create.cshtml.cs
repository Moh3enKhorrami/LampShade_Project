using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
	public class CreateModel : PageModel
    {
        public CreateProduct Command;
        private readonly IProductApplication _productApplication;
        
        public CreateModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
            Command = new CreateProduct();
        }
        
        public void OnGet()
        {
        }

        public IActionResult OnPostCreate(CreateProduct Command)
        {
            var result = _productApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return Page();
            }
        }
    }
}
