using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.Product;
using ShopManagement.Application.Contracts.ProductCategory;
using ShopManagement.Application.Contracts.ProductPicture;
using ShopManagement.Domain.PictureAgg;
using ShopManagement.Infrastructure.EFCore.Repository;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        //[TempData] public string Message { get; set; }
        public ProductPictureSearchModel SearchModel;
        public List<ProductPictureViewModel> ProductPictureViewModels; 
        public SelectList Products;
        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;
        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productApplication = productApplication;
            _productPictureApplication = productPictureApplication;
            SearchModel = new ProductPictureSearchModel();
            ProductPictureViewModels = new List<ProductPictureViewModel>();
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
        }
        public void OnGet(ProductPictureSearchModel searchModel)
        {
            ProductPictureViewModels = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetRemove(long id)
        {
            var result = _productPictureApplication.Remove(id);
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

        public IActionResult OnGetRestore(long id)
        {
            var result = _productPictureApplication.Restore(id);
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
