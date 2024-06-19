using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;

public class ProductCategoryViewComponent : ViewComponent
{
    public ProductCategoryViewComponent(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    private readonly IProductCategoryQuery _productCategoryQuery;


    public IViewComponentResult Invoke()
    {
        var productCategory = _productCategoryQuery.GetProductCategories();
        return View(productCategory);
    }
}