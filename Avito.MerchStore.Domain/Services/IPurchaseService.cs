using Avito.MerchStore.Domain.Models;

namespace Avito.MerchStore.Domain.Services;

public interface IPurchaseService
{
    Task<ErrorResponse?> BuyItem(string username, string itemName, int quantity);
}