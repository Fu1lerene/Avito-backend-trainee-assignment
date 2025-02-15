namespace Avito.MerchStore.Domain.Repositories.Models;

public class User
{
    public long Id { get; init; }
    
    public string Username { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
    
    public int Balance { get; set; } = 1000;
    
    public List<UserInventory> Inventory { get; set; } = new();
}