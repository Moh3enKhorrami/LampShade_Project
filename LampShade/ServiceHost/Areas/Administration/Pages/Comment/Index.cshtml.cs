using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ServiceHost.Areas.Administration.Pages.Comment;

public class IndexModel : PageModel
{
    public CommentSearchModel SearchModel;
    public List<CommentViewModel> Comments;
    private readonly ICommentApplication _commentApplication;

    public IndexModel(ICommentApplication commentApplication)
    {
        _commentApplication = commentApplication;
    }
    
    public void OnGet(CommentSearchModel searchModel)
    {
        Comments = _commentApplication.Search(searchModel);
    }

    public IActionResult OnGetCancel(long id)
    {
        var result = _commentApplication.Cancel(id);
        return RedirectToPage("./Index");
    }

    public IActionResult OnGetConfirm(long id)
    {
        var result = _commentApplication.Confirm(id);
        return RedirectToPage("./Index");
        
    }
    
}