using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class SentCoins
{
    /// <summary>
    /// Имя пользователя, которому отправлены монеты.
    /// </summary>
    [JsonPropertyName("toUserName")]
    public string ToUserName { get; set; } = String.Empty;

    /// <summary>
    /// Количество отправленных монет.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}