using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class EditModel : PageModel
    {
        public EditProduct Command;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public SelectList ProductCategories;

        public EditModel(IProductApplication productApplication, IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            _productApplication = productApplication;
            Command = new EditProduct();
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }

        public void OnGet(long id)
        {
            Command = _productApplication.GetDetails(id);

        }

        public IActionResult OnPostEdit(EditProduct Command)
        {
            var result = new OperationResult();
            result = _productApplication.Edit(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                //return Page();
            }
            
        }
       
    }
}