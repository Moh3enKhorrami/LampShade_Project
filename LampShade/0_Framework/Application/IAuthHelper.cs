using System.ComponentModel.Design;

namespace _0_Framework.Application;

public interface IAuthHelper
{
    void Signin(AuthViewModel account);
    void SignOut();
    bool IsAuthenticated();
    
}