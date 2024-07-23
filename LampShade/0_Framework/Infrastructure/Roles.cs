namespace _0_Framework.Infrastructure;

public static class Roles
{
    public const string Administrator = "1";
    public const string Client = "2";
    public const string Creator = "3";

    public static string GetRoleBy(long id)
    {
        switch (id)
        {
            case 1:
                return "Administrator";
            case 3:
                return "Creator";
            default:
                return "Client";
        }
    }
}