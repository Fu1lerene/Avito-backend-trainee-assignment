using Avito.MerchStore.Domain.Models;

namespace Avito.MerchStore.Domain.Services;

public interface ICoinService
{
    public Task<ErrorResponse?> SendCoins(string senderName, string receiverName, int amount);
}