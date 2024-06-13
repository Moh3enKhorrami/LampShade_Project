using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class EditModel : PageModel
    {
        public EditProductCategory Command;
        private readonly IProductCategoryApplication _productCategoryApplication;

        public EditModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            Command = new EditProductCategory();
        }

        public void OnGet(long id)
        {
            Command = _productCategoryApplication.GetDetails(id);

        }

        public IActionResult OnPostEdit(EditProductCategory Command)
        {
            var result = new OperationResult();
            result = _productCategoryApplication.Edit(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                //return Page();
            }
            
        }

        //public IActionResult OnPostShow(EditProductCategory command)
        //{
           // var result = _productCategoryApplication.Edit(command);
         //   return RedirectToPage(result);
        //}
       
       
    }
}