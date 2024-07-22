using System.Security.AccessControl;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Domain.AccountAgg;

namespace AccountManagement.Application;

public class AccountApplication : IAccountApplication
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly IAccountRepository _accountRepository;
    private readonly IFileUpLoader _fileUpLoader;
    private readonly IAuthHelper _authHelper;

    public AccountApplication(IAccountRepository accountRepository, IPasswordHasher passwordHasher,
        IFileUpLoader fileUpLoader, IAuthHelper authHelper)
    {
        _authHelper = authHelper;
        _accountRepository = accountRepository;
        _passwordHasher = passwordHasher;
        _fileUpLoader = fileUpLoader;
    }

    public OperationResult Create(CreateAccount command)
    {
        var operation = new OperationResult();
        if (_accountRepository.Exists(x => x.UserName == command.UserName || x.Mobile == command.Mobile))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var password = _passwordHasher.Hash(command.Password);
        var path = $"profilePhotos";
        var picturePath = _fileUpLoader.UpLoad(command.ProfilePhoto,path);
        var account = new Account(command.FullName, command.UserName, password, command.Mobile, command.RoleId,
            picturePath);
        _accountRepository.Create(account);
        _accountRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult Edit(EditAccount command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (_accountRepository.Exists(x => (x.UserName == command.UserName || x.Mobile == command.Mobile)
                                           && x.Id != command.Id))
            return operation.Failed(ApplicationMessages.DuplicatedRecord);
        var path = $"profilePhotos";
        var picturePath = _fileUpLoader.UpLoad(command.ProfilePhoto,path);
        account.Edit(command.FullName, command.UserName, command.Mobile, command.RoleId, picturePath);
        _accountRepository.SaveChanges();
        return operation.Succedded();
    }

    public OperationResult ChangePassword(ChangePassword command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.Get(command.Id);
        if (account == null)
            return operation.Failed(ApplicationMessages.RecordNotFound);
        if (command.Password != command.RePassword)
            operation.Failed(ApplicationMessages.PasswordsNotMatch);
        var password = _passwordHasher.Hash(command.Password);
        account.ChangePassword(password);
        _accountRepository.SaveChanges();
        return operation.Succedded();
    }

    public EditAccount GetDetails(long id)
    {
        return _accountRepository.GetDetails(id);
    }

    public ChangePassword GetChange(long id)
    {
        return _accountRepository.GetChange(id);
    }

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        return _accountRepository.Search(searchModel);
    }

    public OperationResult Login(Login command)
    {
        var operation = new OperationResult();
        var account = _accountRepository.GetBy(command.UserName);
        if (account == null)
            return operation.Failed(ApplicationMessages.UserOrPassNotExists);
        (bool verified,bool needsUpgrade) result = _passwordHasher.Check(account.Password, command.Password);
        if (!result.verified)
            return operation.Failed(ApplicationMessages.UserOrPassNotExists);
        var authViewModel = new AuthViewModel(account.Id, account.RoleId, account.FullName,account.UserName);
        _authHelper.Signin(authViewModel);
        return operation.Succedded();
        
    }

    public void LogOut()
    {
        _authHelper.SignOut();
    }
}