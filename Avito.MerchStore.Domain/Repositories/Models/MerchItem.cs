namespace Avito.MerchStore.Domain.Repositories.Models;

public class MerchItem
{
    public long Id { get; init; }
    public string? Name { get; set; }
    public int Price { get; set; }
    
    public List<UserInventory> Inventory { get; set; } = new();
}