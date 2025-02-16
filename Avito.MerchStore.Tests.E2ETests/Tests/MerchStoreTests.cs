using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Tests.E2E.HttpClients;
using FluentAssertions;

namespace Avito.MerchStore.Tests.E2E.Tests;

public class MerchStoreTests
{
    private readonly E2EClient _e2EClient;

    public MerchStoreTests()
    {
        _e2EClient = new E2EClient();
    }
    
    [Fact]
    public async Task SendCoins_ValidData_ShouldTransferCoins()
    {
        var username1 = "TestUser1";
        var username2 = "TestUser2";
        var amount = 10;
        await _e2EClient.Authenticate(username2);
        await _e2EClient.Authenticate(username1);
        
        var user1 = await _e2EClient.GetUserByName(username1);
        var user2 = await _e2EClient.GetUserByName(username2);
        var expectedBalanceUser1 = user1.Balance - amount;
        var expectedBalanceUser2 = user2.Balance + amount;
        
        await _e2EClient.SendCoin(new SendCoinRequest
        {
            ToUser = username2,
            Amount = amount
        });
        user1 = await _e2EClient.GetUserByName(username1);
        user2 = await _e2EClient.GetUserByName(username2);

        user1.Balance.Should().Be(expectedBalanceUser1);
        user2.Balance.Should().Be(expectedBalanceUser2);
    }

    [Fact]
    public async Task BuyItem_ValidData_ShouldBuyItemAndSpendCoins()
    {
        var username = "TestUser3";
        await _e2EClient.Authenticate(username);
        var itemName1 = "socks";
        var itemName2 = "pen";
        var quantity = 2;
        
        var user = await _e2EClient.GetUserByName(username);
        var item1 = await _e2EClient.GetItemByName(itemName1);
        var item2 = await _e2EClient.GetItemByName(itemName2);
        var userInfo = await _e2EClient.GetInfo();

        var expectedBalance = user.Balance - item1.Price - item2.Price*quantity;

        var expectedInventory = userInfo.Inventory is null ? new List<Item>() : userInfo.Inventory.ToList();
        var existingitem1 = userInfo.Inventory?.FirstOrDefault(x => x.Type == itemName1);
        var existingitem2 = userInfo.Inventory?.FirstOrDefault(x => x.Type == itemName2);

        if (existingitem1 is null)
        {
            expectedInventory.Add(new Item
            {
                Type = itemName1,
                Quantity = 1
            });
        }
        else
        {
            existingitem1.Quantity += 1;
        }

        if (existingitem2 is null)
        {
            expectedInventory.Add(new Item
            {
                Type = itemName2,
                Quantity = quantity
            });
        }
        else
        {
            existingitem2.Quantity += quantity;
        }
            
        await _e2EClient.BuyItem(itemName1, 1);
        await _e2EClient.BuyItem(itemName2, quantity);

        userInfo = await _e2EClient.GetInfo();
        
        userInfo.Coins.Should().Be(expectedBalance);
        userInfo.Inventory.Should().BeEquivalentTo(expectedInventory);
    }
    
    [Fact]
    public async Task GetInfo_CoinHistory_ShouldBeCorrectHistory()
    {
        var username1 = "TestUser1";
        var username2 = "TestUser2";
        var username3 = "TestUser3";
        var amount = 10;
        
        await _e2EClient.Authenticate(username1);
        var userInfo = await _e2EClient.GetInfo();

        await _e2EClient.Authenticate(username2);
        await _e2EClient.SendCoin(new SendCoinRequest
        {
            ToUser = username1,
            Amount = amount
        });
        
        await _e2EClient.Authenticate(username3);
        await _e2EClient.SendCoin(new SendCoinRequest
        {
            ToUser = username1,
            Amount = amount
        });
        
        await _e2EClient.Authenticate(username1);
        await _e2EClient.SendCoin(new SendCoinRequest
        {
            ToUser = username2,
            Amount = amount
        });
        
        var expectedSentCoins = userInfo.CoinHistory.SentCoins.ToList();
        expectedSentCoins.Add(new SentCoins
        {
            ToUserName = username2,
            Amount = amount
        });
        
        var expectedReceivedCoins = userInfo.CoinHistory.ReceivedCoins.ToList();
        expectedReceivedCoins.Add(new ReceivedCoins
        {
            FromUserName = username2,
            Amount = amount
        });
        expectedReceivedCoins.Add(new ReceivedCoins
        {
            FromUserName = username3,
            Amount = amount
        });

        userInfo = await _e2EClient.GetInfo();

        userInfo.CoinHistory?.SentCoins.Should().BeEquivalentTo(expectedSentCoins);
        userInfo.CoinHistory?.ReceivedCoins.Should().BeEquivalentTo(expectedReceivedCoins);
    }
    
}