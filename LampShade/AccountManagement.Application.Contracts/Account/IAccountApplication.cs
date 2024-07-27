using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.Account;

public interface IAccountApplication
{
    void LogOut();
    EditAccount GetDetails(long id);
    ChangePassword GetChange(long id);
    OperationResult Login(Login command);
    OperationResult Edit(EditAccount command);
    OperationResult Create(CreateAccount command);
    OperationResult ChangePassword(ChangePassword command);
    List<AccountViewModel> Search(AccountSearchModel searchModel);
}