using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class CoinHistory
{
    [JsonPropertyName("receivedCoins")]
    public IReadOnlyCollection<ReceivedCoins>? ReceivedCoins { get; set; }
    
    [JsonPropertyName("sentCoins")]
    public IReadOnlyCollection<SentCoins>? SentCoins { get; set; }
}