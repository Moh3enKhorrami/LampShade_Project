using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Role;

public interface IRoleApplication
{
    List<RoleViewModel> List();
    EditRole GetDetails(long id);
    OperationResult Edit(EditRole command);
    OperationResult Create(CreateRole command);
}