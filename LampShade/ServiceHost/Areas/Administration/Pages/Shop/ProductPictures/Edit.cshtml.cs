using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class EditModel : PageModel
    {
        public EditProductPicture _command;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public SelectList Products;

        public EditModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
            _command = new EditProductPicture();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public void OnGet(long id)
        {
            _command = _productPictureApplication.GetDetails(id);

        }

        public IActionResult OnPostEdit(EditProductPicture Command)
        {
            var result = new OperationResult();
            result = _productPictureApplication.Edit(Command);
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