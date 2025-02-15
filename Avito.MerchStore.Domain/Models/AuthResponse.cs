namespace Avito.MerchStore.Domain.Models;

public class AuthResponse
{
    /// <summary>
    /// JWT-токен для доступа к защищенным ресурсам.
    /// </summary>
    public string Token { get; set; } = string.Empty;
}