namespace Avito.MerchStore.Domain.Repositories.Models;

public class UserInventory
{
    public long UserId { get; set; }
    public long ItemId { get; set; }
    public int Quantity { get; set; }
    
    public User User { get; set; }
    public MerchItem Item { get; set; }
}