using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class IndexModel : PageModel
    {
        public List<RoleViewModel> Roles; 
        private readonly IRoleApplication _roleApplication;
        public IndexModel( IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }
        public void OnGet()
        {
            Roles = _roleApplication.List();
        }
        
    }
       
        
}
