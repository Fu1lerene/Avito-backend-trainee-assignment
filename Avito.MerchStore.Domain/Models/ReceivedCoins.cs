using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class ReceivedCoins
{
    /// <summary>
    /// Имя пользователя, который отправил монеты.
    /// </summary>
    [JsonPropertyName("fromUserName")]
    public string FromUserName { get; set; } = String.Empty;

    /// <summary>
    /// Количество полученных монет.
    /// </summary>
    [JsonPropertyName("amount")]
    public int Amount { get; set; }
}