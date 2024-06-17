using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
	public class CreateModel : PageModel
    {
        public CreateProductPicture _command;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public SelectList Products;
        public CreateModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPostCreate(CreateProductPicture Command)
        {
            
            var result = _productPictureApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
