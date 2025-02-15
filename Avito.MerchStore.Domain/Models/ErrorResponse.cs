namespace Avito.MerchStore.Domain.Models;

public class ErrorResponse
{
    /// <summary>
    /// Сообщение об ошибке, описывающее проблему.
    /// </summary>
    public string ErrorMessage { get; set; } = String.Empty;
}