namespace Avito.MerchStore.Domain.Models;

public class InfoResponse
{
    /// <summary>
    /// Количество доступных монет.
    /// </summary>
    public int Coins { get; set; }

    public IReadOnlyCollection<Item>? Inventory { get; set; } = null;
    
    public CoinHistory? CoinHistory { get; set; } = null;
}