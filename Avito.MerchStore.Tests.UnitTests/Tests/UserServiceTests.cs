using AutoFixture;
using Avito.MerchStore.API.Services;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Repositories.Models;
using Avito.MerchStore.Domain.Services;
using FluentAssertions;
using Moq;

namespace Avito.MerchStore.Tests.UnitTests.Tests;

public class UserServiceTests
{
    private readonly Mock<IMerchStoreRepository> _repositoryMock;
    private readonly IUserService _userService;

    public UserServiceTests()
    {
        _repositoryMock = new Mock<IMerchStoreRepository>();
        _userService = new UserService(_repositoryMock.Object);
    }
    
    [Fact]
    public async Task GetUserByName_CorrectData_ShouldReturnUser()
    {
        var username = "Test";
        var user = new User { Username = username, Balance = 1000 };
        
        _repositoryMock.Setup(x => x.GetUserByName(username)).ReturnsAsync(user);
        
        var response = await _userService.GetUserByName(username);
    
        response?.Should().NotBeNull();
        _repositoryMock.Verify(x => x.GetUserByName(It.IsAny<string>()), Times.Once);
    }
    
    [Fact]
    public async Task GetInfo_CorrectData_ShouldReturnInfoResponse()
    {
        var fixture = new Fixture();
        var username = "Test";
        
        var user = new User { Username = username, Balance = 1000 };
        var item = new MerchItem { Name = "t-shirt", Price = 80 };
        var inventory = fixture.Build<UserInventory>()
            .With(i => i.User, user)
            .With(i => i.Item, item)
            .CreateMany(3).ToList();
        user.Inventory = inventory;
        item.Inventory = inventory;
        
        var transactionHistory = fixture.Build<Transaction>()
            .With(t => t.SenderName, username)
            .CreateMany(4).ToList();

        foreach (var transaction in transactionHistory)
        {
            transaction.SenderName = username;
            transaction.ReceiverName = "receiver";
        }
        
        _repositoryMock.Setup(x => x.GetUserByName(user.Username)).ReturnsAsync(user);
        _repositoryMock.Setup(x => x.GetUserInventory(user.Username)).ReturnsAsync(inventory);
        _repositoryMock.Setup(x => x.GetTransactionsHistory(user.Username)).ReturnsAsync(transactionHistory);

        var response = await _userService.GetInfo(user.Username);

        response.Should().NotBeNull();
        response.Coins.Should().Be(user.Balance);
        response.Inventory.Should().HaveCount(3);
        response.CoinHistory?.SentCoins.Should().HaveCount(4);
    }
}