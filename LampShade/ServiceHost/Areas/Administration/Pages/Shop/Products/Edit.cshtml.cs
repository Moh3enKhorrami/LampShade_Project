using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Product;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class EditModel : PageModel
    {
        public EditProduct Command;
        private readonly IProductApplication _productApplication;

        public EditModel(IProductApplication productApplication)
        {
            _productApplication = productApplication;
            Command = new EditProduct();
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