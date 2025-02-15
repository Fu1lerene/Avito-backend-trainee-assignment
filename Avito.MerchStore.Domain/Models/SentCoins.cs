namespace Avito.MerchStore.Domain.Models;

public class SentCoins
{
    /// <summary>
    /// Имя пользователя, которому отправлены монеты.
    /// </summary>
    public string ToUserName { get; set; } = String.Empty;

    /// <summary>
    /// Количество отправленных монет.
    /// </summary>
    public int Amount { get; set; }
}