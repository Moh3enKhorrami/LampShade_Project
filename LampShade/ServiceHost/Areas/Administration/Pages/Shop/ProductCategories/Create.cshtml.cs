using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
	public class CreateModel : PageModel
    {
        private readonly CreateProductCategory _command;
        private readonly IProductCategoryApplication _productCategoryApplication;
        
        public CreateModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }
        
        public void OnGet()
        {
        }

        public void OnPostCreate(CreateProductCategory command)
        {
            var result = _productCategoryApplication.Create(command);
        }
    }
}
