namespace IdentityServer.Services
{
    public interface IPasswordValidationService
    {
        string Encrypt(string password);
        bool IsMatch(string encrypted, string entered);
    }
}