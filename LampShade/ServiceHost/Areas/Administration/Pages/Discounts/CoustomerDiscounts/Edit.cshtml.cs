using _0_Framework.Application;
using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CoustomerDiscounts
{
    public class EditModel : PageModel
    {
        public EditCustomerDiscount Command;
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        public SelectList Products;

        public EditModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
            Command = new EditCustomerDiscount();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }

        public void OnGet(long id)
        {
            Command = _customerDiscountApplication.GetDetails(id);
        }

        public IActionResult OnPostEdit( EditCustomerDiscount Command)
        {
            var result = _customerDiscountApplication.Edit(Command);
            if (result != null)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                //return Page();
            }
            
        }
       
    }
}