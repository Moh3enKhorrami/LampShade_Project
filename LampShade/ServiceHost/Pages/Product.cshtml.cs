using _01_LampshadeQuery.Contracts.Product;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages.Shared;

public class Product : PageModel
{
    public ProductQueryModel ProductQueryModel;
    private readonly ICommentApplication _commentApplication;
    private readonly IProductQuery _productQuery;
    public Product(IProductQuery productQuery, ICommentApplication commentApplication)
    {
        _productQuery = productQuery;
        _commentApplication = commentApplication;
    }

    public void OnGet(string id)
    {
        ProductQueryModel = _productQuery.GetDetails(id);
    }

    public IActionResult OnPost(AddComment command, string productSlug)
    {
        command.Type = CommentTypes.Product;
        var result =_commentApplication.Add(command);
        return RedirectToPage("/Product", new { Id = productSlug });
    }
} 