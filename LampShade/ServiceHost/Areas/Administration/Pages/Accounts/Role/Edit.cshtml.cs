using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public EditRole Command;
        private readonly IRoleApplication _roleApplication;
        public EditModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
            Command = new EditRole();

        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);

        }

        public IActionResult OnPostEdit(EditRole Command)
        {
            var result = new OperationResult();
            result = _roleApplication.Edit(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                
            }
            
        }
       
    }
}