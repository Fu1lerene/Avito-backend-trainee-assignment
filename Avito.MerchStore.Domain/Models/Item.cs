namespace Avito.MerchStore.Domain.Models;

public class Item
{
    /// <summary>
    /// Тип предмета.
    /// </summary>
    public string? Type { get; set; } = String.Empty;

    /// <summary>
    /// Количество предметов.
    /// </summary>
    public int Quantity { get; set; }
}