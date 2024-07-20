using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
	public class CreateModel : PageModel
    {
        public CreateRole Command;
        private readonly IRoleApplication _roleApplication;
        public CreateModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }
        
        public void OnGet()
        {
            
        }

        public IActionResult OnPostCreate(CreateRole Command)
        {
            var result = _roleApplication.Create(Command);
            if (result.IsSuccedded)
                return RedirectToPage("./Index") ;
            else
            {
                return Page();
            }
        }
    }
}
