namespace Avito.MerchStore.Domain.Models;

public class ReceivedCoins
{
    /// <summary>
    /// Имя пользователя, который отправил монеты.
    /// </summary>
    public string FromUserName { get; set; } = String.Empty;

    /// <summary>
    /// Количество полученных монет.
    /// </summary>
    public int Amount { get; set; }
}