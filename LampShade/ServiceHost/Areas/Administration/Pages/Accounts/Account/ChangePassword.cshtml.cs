using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class ChangePasswordModel : PageModel
    {
        public ChangePassword Command;
        private readonly IAccountApplication _accountApplication;
        
        public ChangePasswordModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
            

        }

        public void OnGet(long id)
        {
            // Command = _accountApplication.GetChange(id);
            Command = new ChangePassword { Id = id };

        }

        public IActionResult OnPostChangePassword(ChangePassword Command)
        {
            var result = new OperationResult();
            result = _accountApplication.ChangePassword(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./ChangePassword",new { id = Command.Id });
                
            }
            
        }
       
    }
}