using System.Text.Json.Serialization;

namespace Avito.MerchStore.Domain.Repositories.Models;

public class User
{
    [JsonPropertyName("id")]
    public long Id { get; init; }
    
    [JsonPropertyName("username")]
    public string Username { get; set; } = string.Empty;
    
    [JsonIgnore]
    public string PasswordHash { get; set; } = string.Empty;
    
    [JsonPropertyName("balance")]
    public int Balance { get; set; } = 1000;
    
    [JsonPropertyName("inventory")]
    public List<UserInventory> Inventory { get; set; } = new();
}