using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
    public class EditModel : PageModel
    {
        public EditAccount Command;
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;

        public SelectList Roles;

        public EditModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
            _accountApplication = accountApplication;
            Command = new EditAccount();
            Roles = new SelectList(_roleApplication.List(), "Id", "Name");

        }

        public void OnGet(long id)
        {
            Command = _accountApplication.GetDetails(id);

        }

        public IActionResult OnPostEdit(EditAccount Command)
        {
            var result = new OperationResult();
            result = _accountApplication.Edit(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index");
            else
            {
                return RedirectToPage("./Edit", new {id = Command.Id });
                
            }
            
        }
       
    }
}