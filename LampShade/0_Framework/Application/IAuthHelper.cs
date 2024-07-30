using System.ComponentModel.Design;

namespace _0_Framework.Application;

public interface IAuthHelper
{
    void SignOut();
    bool IsAuthenticated(); // Is it logged in or not?
    List<int> GetPermissions();
    string CurrentAccountRole();
    void Signin(AuthViewModel account);
    AuthViewModel CurrentAccountInfo();
    long CurrentAccountId();
    
}