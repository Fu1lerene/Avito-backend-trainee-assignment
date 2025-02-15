namespace Avito.MerchStore.Domain.Models;

public class CoinHistory
{
    public IReadOnlyCollection<ReceivedCoins>? ReceivedCoins { get; set; } = null;
    public IReadOnlyCollection<SentCoins>? SentCoins { get; set; } = null;
}