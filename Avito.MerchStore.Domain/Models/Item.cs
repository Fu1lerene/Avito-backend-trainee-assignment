using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Models;

public class Item
{
    /// <summary>
    /// Тип предмета.
    /// </summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; } = String.Empty;

    /// <summary>
    /// Количество предметов.
    /// </summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}