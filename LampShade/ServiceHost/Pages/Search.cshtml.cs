using _01_LampshadeQuery.Contracts.Product;
using _01_LampshadeQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages;

public class Search : PageModel
{
    public string Value; 
    public List<ProductQueryModel> Product;
    private readonly IProductQuery _productQuery;

    public Search(IProductQuery productQuery)
    {
        _productQuery = productQuery;
    }

    public void OnGet(string value)
    {
        Value = value;
        Product = _productQuery.Search(value);
    } 
}