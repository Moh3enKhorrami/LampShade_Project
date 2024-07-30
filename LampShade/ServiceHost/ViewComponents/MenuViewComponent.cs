using _01_LampshadeQuery;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;

namespace ServiceHost.ViewComponents;

public class MenuViewComponent: ViewComponent
{
    private readonly IProductCategoryQuery _productCategoryQuery;

    public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public IViewComponentResult Invoke()
    {
        var result = new MenuModel
        {
            ProductCategories = _productCategoryQuery.GetProductCategories(),
        };
        return View(result);
    }
}