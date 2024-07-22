namespace _0_Framework.Application;

public class AuthViewModel
{
    public AuthViewModel(long id, long roleId, string fullName, string userName)
    {
        Id = id;
        RoleId = roleId;
        FullName = fullName;
        UserName = userName;
    }

    public long Id { get; set; }
    public long RoleId { get; set; }
    public string FullName { get; set; }
    public string UserName { get; set; }
    
}