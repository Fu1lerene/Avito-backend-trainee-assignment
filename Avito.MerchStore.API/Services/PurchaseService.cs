using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Services;

namespace Avito.MerchStore.API.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IUserService _userService; 
    private readonly IMerchStoreRepository _merchStoreRepository;

    public PurchaseService(IUserService userService,
        IMerchStoreRepository merchStoreRepository)
    {
        _userService = userService;
        _merchStoreRepository = merchStoreRepository;
    }
    
    public async Task<ErrorResponse?> BuyItem(string username, string itemName, int quantity)
    {
        if (quantity < 1)
        {
            return new ErrorResponse { ErrorMessage = "Количество товаров должно быть больше 0" };
        }
        
        var item = await _merchStoreRepository.GetItemByName(itemName);
        var user = await _userService.GetUserByName(username);

        if (item is null)
        {
            return new ErrorResponse { ErrorMessage = "Товар с таким названием не найден" };
        }

        if (user!.Balance < item.Price * quantity)
        {
            return new ErrorResponse { ErrorMessage = "Недостаточно монет для покупки" };
        }
        
        await _merchStoreRepository.BuyItem(user, item, quantity);
        
        return null;
    }
}