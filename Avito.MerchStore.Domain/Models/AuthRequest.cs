using System.ComponentModel.DataAnnotations;

namespace Avito.MerchStore.Domain.Models;

public class AuthRequest
{
    /// <summary>
    /// Имя пользователя для аутентификации.
    /// </summary>
    [Required]
    public string Username { get; set; } = string.Empty;
    
    /// <summary>
    /// Пароль для аутентификации.
    /// </summary>
    [Required]
    public string Password { get; set; } = String.Empty;
    
}