using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Services;

namespace Avito.MerchStore.API.Services;

public class CoinService : ICoinService
{
    private readonly IUserService _userService;
    private readonly IMerchStoreRepository _merchStoreRepository;
    
    public CoinService(IUserService userService,
        IMerchStoreRepository merchStoreRepository)
    {
        _userService = userService;
        _merchStoreRepository = merchStoreRepository;
    }

    public async Task<ErrorResponse?> SendCoins(string senderName, string receiverName, int amount)
    {
        if (amount < 1)
        {
            return new ErrorResponse { ErrorMessage = "Минимальное количество для передачи монет: 1" };
        }
        
        var senderUser = await _userService.GetUserByName(senderName);
        var receiverUser = await _userService.GetUserByName(receiverName);

        if (senderUser is null || receiverUser is null || senderName == receiverName)
        {
            return new ErrorResponse { ErrorMessage = "Некорректный получатель" };
        }
        if (senderUser.Balance < amount)
        {
            return new ErrorResponse { ErrorMessage = "Недостаточно монет для перевода" };
        }

        await _merchStoreRepository.SendCoins(senderUser, receiverUser, amount);
        
        return null;
    }
}