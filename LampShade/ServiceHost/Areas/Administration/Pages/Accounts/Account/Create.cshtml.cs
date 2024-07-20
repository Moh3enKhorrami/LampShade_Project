using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Account
{
	public class CreateModel : PageModel
    {
        public CreateAccount Command;
        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public SelectList Roles;
        public CreateModel(IAccountApplication accountApplication, IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
            Roles = new SelectList(_roleApplication.List(),"Id","Name");
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPostCreate(CreateAccount Command)
        {
            var result = _accountApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
