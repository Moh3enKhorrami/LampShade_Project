using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide;

public class CreateModel : PageModel
{
    public CreateSlide Command;
    private readonly ISlideApplication _slideApplication;

    public CreateModel(ISlideApplication slideApplication)
    {
        _slideApplication = slideApplication;
        
    }

    public void OnGet()
    {
        
    }

    public IActionResult OnPostCreate(CreateSlide Command)
    {
        var result = _slideApplication.Create(Command);
        if (result.IsSuccedded)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return Page();
        }
    }
}