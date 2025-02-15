namespace Avito.MerchStore.Domain.Repositories.Models;

public class Transaction
{
    public long Id { get; init; }
    public string SenderName { get; set; } = String.Empty;
    public string ReceiverName { get; set; } = String.Empty;
    public int Amount { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}