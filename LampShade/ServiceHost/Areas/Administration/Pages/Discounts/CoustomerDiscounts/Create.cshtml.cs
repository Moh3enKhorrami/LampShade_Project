using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CoustomerDiscounts
{
	public class CreateModel : PageModel
    {
        public DefineCustomerDiscount Command;
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        public SelectList Products;
        public CreateModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
            //Command = new CreateProduct();
            Products = new SelectList(_productApplication.GetProducts(),"Id","Name");
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPostCreate(DefineCustomerDiscount Command)
        {
            var result = _customerDiscountApplication.Define(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
