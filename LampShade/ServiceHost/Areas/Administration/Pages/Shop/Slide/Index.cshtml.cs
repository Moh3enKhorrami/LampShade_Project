using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide;

public class IndexModel : PageModel
{
    public List<SlideViewModel> Sildes;
    private readonly ISlideApplication _slideApplication;

    public IndexModel(ISlideApplication slideApplication)
    {
        _slideApplication = slideApplication;
    }
    
    public void OnGet()
    {
        Sildes = _slideApplication.GetList();
    }

    public IActionResult OnGetRemove(long id)
    {
        var result = _slideApplication.Remove(id);
        if (result.IsSuccedded)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return RedirectToPage("./Index");
        }
    }

    public IActionResult OnGetRestore(long id)
    {
        var result = _slideApplication.Restore(id);
        if (result.IsSuccedded)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return RedirectToPage("./Index");
        }
    }
    
}