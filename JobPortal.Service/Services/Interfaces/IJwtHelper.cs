namespace JobPortal.ApiHelper
{
    public interface IJwtHelper
    {
        string GenerateToken(string userName, string[] roles, string Email);
    }
}
