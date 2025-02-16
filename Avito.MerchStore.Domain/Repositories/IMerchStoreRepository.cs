using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Repositories.Models;

namespace Avito.MerchStore.Domain.Repositories;

public interface IMerchStoreRepository
{
    Task<User?> GetUserByName(string name);
    Task<MerchItem?> GetItemByName(string name);
    Task SendCoins(User senderUser, User receiverUser, int amount);
    Task BuyItem(User user, MerchItem item, int quantity);
    Task<IReadOnlyCollection<UserInventory>> GetUserInventory(string username);
    Task<IReadOnlyCollection<Transaction>> GetTransactionsHistory();
}