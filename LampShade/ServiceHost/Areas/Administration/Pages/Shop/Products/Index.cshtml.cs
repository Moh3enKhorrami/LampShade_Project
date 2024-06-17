using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        //[TempData] public string Message { get; set; }
        public ProductSearchModel SearchModel;
        public List<ProductViewModel> Products; 
        public SelectList ProductCategories;
        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;
        public IndexModel(IProductApplication productApplication , IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
            _productApplication = productApplication;
            SearchModel = new ProductSearchModel();
            Products = new List<ProductViewModel>();
            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");
        }
        public void OnGet(ProductSearchModel searchModel)
        {
            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetNotInStock(long id)
        {
            var result =_productApplication.NotInStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                //Message = result.Message;
                return RedirectToPage("./Index");
            }
        }

        public IActionResult OnGetIsInStock(long id)
        {
            var result = _productApplication.IsStock(id);
            if (result.IsSuccedded)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                //Message = result.Message;
                return RedirectToPage("./Index");
            }
        }

    }
       
        
}
