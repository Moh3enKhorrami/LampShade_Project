using _0_Framework.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.Slide;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slide;

public class EditModel : PageModel
{
    public EditSlide _command;
    private readonly ISlideApplication _slideApplication;

    public EditModel(ISlideApplication slideApplication)
    {
        _slideApplication = slideApplication;
        _command = new EditSlide();
    }

    public void OnGet(long id)
    {
        _command = _slideApplication.GetDetails(id);
    }

    public IActionResult OnPostEdit(EditSlide command)
    {
        var resuld = new OperationResult(); 
           resuld = _slideApplication.Edit(command);
        if (resuld.IsSuccedded)
        {
            return RedirectToPage("./Index");
        }
        else
        {
            return RedirectToPage("./Edit", new {id = command.Id });
                
        }
        
    }
}