using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Repositories.Models;

public class MerchItem
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    
    [JsonPropertyName("price")]
    public int Price { get; set; }
    
    public List<UserInventory> Inventory { get; set; } = new();
}