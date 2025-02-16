using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Repositories.Models;
using Avito.MerchStore.Domain.Services;

namespace Avito.MerchStore.API.Services;

public class UserService : IUserService
{
    private readonly IMerchStoreRepository _merchStoreRepository;

    public UserService(IMerchStoreRepository merchStoreRepository)
    {
        _merchStoreRepository = merchStoreRepository;
    }
    
    public async Task<User?> GetUserByName(string username)
    {
        return await _merchStoreRepository.GetUserByName(username);
    }

    public async Task<InfoResponse> GetInfo(string username)
    {
        var user = await _merchStoreRepository.GetUserByName(username);
        var inventory = await _merchStoreRepository.GetUserInventory(username);
        var transactionHistory = await _merchStoreRepository.GetTransactionsHistory(username);

        var a = transactionHistory.Where(t => t.SenderName == username).Select(x => new SentCoins
            {
                Amount = x.Amount,
                ToUserName = x.ReceiverName
            }).ToList();
        var coinHistory = new CoinHistory
        {
            SentCoins = transactionHistory.Where(t => t.SenderName == username).Select(x => new SentCoins
            {
                Amount = x.Amount,
                ToUserName = x.ReceiverName
            }).ToList(),
            ReceivedCoins = transactionHistory.Where(t => t.ReceiverName == username).Select(x => new ReceivedCoins
            {
                Amount = x.Amount,
                FromUserName = x.SenderName
            }).ToList()
        };
        
        return new InfoResponse
        {
            Coins = user.Balance,
            CoinHistory = coinHistory,
            Inventory = inventory.Select(ui => new Item
            {
                Quantity = ui.Quantity,
                Type = ui.Item.Name
            }).ToList()
        };
    }
}