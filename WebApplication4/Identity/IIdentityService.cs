namespace WebApplication4.Identity
{
    public interface IIdentityService
    {
        string GenerateJwt(string secret, string userId, string userName, string role);
    }
}
