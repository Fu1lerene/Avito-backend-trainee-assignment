namespace Avito.MerchStore.Domain.Services;

public interface IAuthService
{
    Task<string?> Authenticate(string username, string password);
}