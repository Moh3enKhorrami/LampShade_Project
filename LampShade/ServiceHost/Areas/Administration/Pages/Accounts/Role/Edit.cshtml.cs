using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceHost.Areas.Administration.Pages.Accounts.Role
{
    public class EditModel : PageModel
    {
        public List<SelectListItem> Permissions = new();
        public EditRole Command;
        private readonly IRoleApplication _roleApplication;
        private readonly IEnumerable<IPermissionExposer> _exposers;
        public EditModel(IRoleApplication roleApplication, IEnumerable<IPermissionExposer> exposers)
        {
            _roleApplication = roleApplication;
            Command = new EditRole();
            _exposers = exposers;

        }

        public void OnGet(long id)
        {
            Command = _roleApplication.GetDetails(id);
            
            var permissions = new List<PermissionDto>();
            foreach (var exposer in _exposers)
            {
                var exposedPermissions = exposer.Expose();
                foreach (var (key, value) in exposedPermissions)
                {
                    permissions.AddRange(value);
                    var group = new SelectListGroup
                    {
                        Name = key
                    };
                    foreach (var permission in value)
                    {
                        var item = new SelectListItem(permission.Name, permission.Code.ToString())
                        {
                            Group = group
                        };
                        if (Command.MappedPermissions.Any(x => x.Code == permission.Code))
                            item.Selected = true;
                        
                        Permissions.Add(item);
                    }
                }
            }

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