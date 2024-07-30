using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Domain.RoleAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountMangement.Infrastructure.EFCore.Repository;

public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
{
    private AccountContext _accountContext;
    public RoleRepository(AccountContext accountContext) : base(accountContext)
    {
        _accountContext = accountContext;
    }

    public List<RoleViewModel> List()
    {
        return _accountContext.Roles.Select(x => new RoleViewModel()
        {
            Id = x.Id,
            Name = x.Name,
            CreationDate = x.CreationDate.ToString()
        }).ToList();
    }

    public EditRole GetDetails(long id)
    {
        var role = _accountContext.Roles.Select(x => new EditRole()
        {
            Id = x.Id,
            Name = x.Name,
            MappedPermissions = MapPermissions(x.Permissions)
        }).AsNoTracking().FirstOrDefault(x => x.Id == id);
        role.Permissions = role.MappedPermissions.Select(x => x.Code).ToList(); 
        return role;
    }
    private static List<PermissionDto> MapPermissions(List<Permission> permissions)
    {
        return permissions.Select(x => new PermissionDto(x.Code, x.Name)).ToList();
    }
}