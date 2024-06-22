using DiscountManagement.Application.Contracts.CustomerDiscount;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Discounts.CoustomerDiscounts
{
    public class IndexModel : PageModel
    {
        public CustomerDiscountSearchModel SearchModel; //?
        public List<CustomerDiscountViewModel> CustomerDiscount; 
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        public IndexModel(IProductApplication productApplication, ICustomerDiscountApplication customerDiscountApplication )
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
            SearchModel = new CustomerDiscountSearchModel();
            CustomerDiscount = new List<CustomerDiscountViewModel>();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
        public void OnGet(CustomerDiscountSearchModel searchModel) //?
        {
            CustomerDiscount = _customerDiscountApplication.Search(searchModel);
        }
        
        

    }
       
        
}
