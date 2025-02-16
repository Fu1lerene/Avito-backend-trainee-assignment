using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class SendCoinRequest
{
    /// <summary>
    /// Имя пользователя, которому нужно отправить монеты.
    /// </summary>
    [Required]
    [JsonPropertyName("toUser")]
    public string ToUser { get; set; } = string.Empty;
    
    /// <summary>
    /// Количество монет, которые необходимо отправить.
    /// </summary>
    [Required]
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}