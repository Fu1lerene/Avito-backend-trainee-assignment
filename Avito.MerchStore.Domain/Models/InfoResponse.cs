using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class InfoResponse
{
    /// <summary>
    /// Количество доступных монет.
    /// </summary>
    [JsonPropertyName("coins")]
    public int Coins { get; set; }

    [JsonPropertyName("inventory")]
    public IReadOnlyCollection<Item>? Inventory { get; set; } = null;
    
    [JsonPropertyName("coinHistory")]
    public CoinHistory? CoinHistory { get; set; } = null;
}