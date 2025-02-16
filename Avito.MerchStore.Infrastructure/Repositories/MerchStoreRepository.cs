using System.Data;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace Avito.MerchStore.Infrastructure.Repositories;

public class MerchStoreRepository : IMerchStoreRepository
{
    private readonly MerchDbContext _context;

    public MerchStoreRepository(MerchDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetUserByName(string name)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == name);
    }

    public async Task<MerchItem?> GetItemByName(string name)
    {
        return await _context.MerchItems.FirstOrDefaultAsync(i => i.Name == name);
    }

    public async Task BuyItem(User user, MerchItem item, int quantity)
    {
        user.Balance -= item.Price * quantity;
        await AddItemToInventory(user, item, quantity);
        
        await _context.SaveChangesAsync();
    }
    
    public async Task SendCoins(User senderUser, User receiverUser, int amount)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            senderUser.Balance -= amount;
            receiverUser.Balance += amount;

            _context.Users.Update(senderUser);
            _context.Users.Update(receiverUser);

            _context.Transactions.Add(new Transaction
            {
                SenderName = senderUser.Username,
                ReceiverName = receiverUser.Username,
                Amount = amount,
                Timestamp = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    
    public async Task<IReadOnlyCollection<UserInventory>> GetUserInventory(string username)
    {
        var user = await _context.Users
            .Include(u => u.Inventory)
            .ThenInclude(ui => ui.Item)
            .FirstOrDefaultAsync(u => u.Username == username);
        
        return user.Inventory;
    }
    
    public async Task<IReadOnlyCollection<Transaction>> GetTransactionsHistory()
    {
        return await _context.Transactions
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    private async Task AddItemToInventory(User user, MerchItem item, int quantity)
    {
        var inventory = await _context.UserInventories.FirstOrDefaultAsync(i => i.UserId == user.Id && i.ItemId == item.Id);
        if (inventory is null)
        {
            inventory = new UserInventory
            {
                UserId = user.Id,
                ItemId = item.Id,
                Quantity = quantity
            };

            await _context.UserInventories.AddAsync(inventory);
        }
        else
        {
            inventory.Quantity += quantity;
            _context.UserInventories.Update(inventory);
        }
    }
}