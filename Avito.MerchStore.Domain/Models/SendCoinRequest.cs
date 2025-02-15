using System.ComponentModel.DataAnnotations;

namespace Avito.MerchStore.Domain.Models;

public class SendCoinRequest
{
    /// <summary>
    /// Имя пользователя, которому нужно отправить монеты.
    /// </summary>
    [Required]
    public string ToUser { get; set; } = string.Empty;
    
    /// <summary>
    /// Количество монет, которые необходимо отправить.
    /// </summary>
    [Required]
    public int Amount { get; set; }
}