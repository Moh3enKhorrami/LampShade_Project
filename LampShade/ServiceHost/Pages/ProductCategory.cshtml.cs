using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;

public class ProductCategory : PageModel
{
    public ProductCategoryQueryModel ProductCategoryQueryModel;
    private readonly IProductCategoryQuery _productCategoryQuery;

    public ProductCategory(IProductCategoryQuery productCategoryQuery)
    {
        _productCategoryQuery = productCategoryQuery;
    }

    public void OnGet(string id)
    {
        ProductCategoryQueryModel = _productCategoryQuery.GetProductCategoryWithProductsBy(id);
    }
}