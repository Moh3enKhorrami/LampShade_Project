using System.ComponentModel.Design;

namespace _0_Framework.Application;

public interface IAuthHelper
{
    void SignOut();
    bool IsAuthenticated();
    string CurrentAccountRole();
    void Signin(AuthViewModel account);
    AuthViewModel CurrentAccountInfo();
}