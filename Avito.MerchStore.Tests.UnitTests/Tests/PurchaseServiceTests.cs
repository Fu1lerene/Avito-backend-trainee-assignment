using Avito.MerchStore.API.Services;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Repositories.Models;
using Avito.MerchStore.Domain.Services;
using FluentAssertions;
using Moq;

namespace Avito.MerchStore.Tests.UnitTests.Tests;

public class PurchaseServiceTests
{
    private readonly Mock<IMerchStoreRepository> _repositoryMock;
    private readonly IPurchaseService _purchaseService;
    private readonly IUserService _userService;

    public PurchaseServiceTests()
    {
        _repositoryMock = new Mock<IMerchStoreRepository>();
        _userService = new UserService(_repositoryMock.Object);
        _purchaseService = new PurchaseService(_userService, _repositoryMock.Object);
    }
    
    [Fact]
    public async Task BuyItem_CorrectData_ShouldReturnNull()
    {
        var username = "Test";
        var itemName = "t-shirt";
        var user = new User { Username = username, Balance = 1000 };
        var item = new MerchItem { Name = itemName, Price = 80 };
        
        _repositoryMock.Setup(x => x.GetUserByName(username)).ReturnsAsync(user);
        _repositoryMock.Setup(x => x.GetItemByName(itemName)).ReturnsAsync(item);
        
        var response = await _purchaseService.BuyItem(username, itemName, 1);

        response?.ErrorMessage.Should().BeNull();
        
        _repositoryMock.Verify(x => x.BuyItem(
                It.IsAny<User>(),
                It.IsAny<MerchItem>(), 
                It.IsAny<int>()),
            Times.Once);
    }

    [Fact]
    public async Task BuyItem_NonPositiveQuantity_ShouldReturnError()
    {
        var username = "Test";
        var itemName = "t-shirt";
        
        var response = await _purchaseService.BuyItem(username, itemName, -1);

        response?.ErrorMessage.Should().Be("Количество товаров должно быть больше 0");
    }
    
    [Fact]
    public async Task BuyItem_NotFoundItem_ShouldReturnError()
    {
        var username = "Test";
        var itemName = "NotExistingItem";
        var user = new User { Username = username, Balance = 1000 };
        var item = new MerchItem { Name = itemName, Price = 80 };
        
        _repositoryMock.Setup(x => x.GetUserByName(username)).ReturnsAsync(user);
        _repositoryMock.Setup(x => x.GetItemByName(itemName)).ReturnsAsync(null as MerchItem);
        
        var response = await _purchaseService.BuyItem(username, itemName, 1);

        response?.ErrorMessage.Should().Be("Товар с таким названием не найден");
    }
    
    [Fact]
    public async Task BuyItem_NotEnoughCoins_ShouldReturnError()
    {
        var username = "Test";
        var itemName = "t-shirt";
        var user = new User { Username = username, Balance = 10 };
        var item = new MerchItem { Name = itemName, Price = 80 };
        
        _repositoryMock.Setup(x => x.GetUserByName(username)).ReturnsAsync(user);
        _repositoryMock.Setup(x => x.GetItemByName(itemName)).ReturnsAsync(item);
        
        var response = await _purchaseService.BuyItem(username, itemName, 1);

        response?.ErrorMessage.Should().Be("Недостаточно монет для покупки");
    }
}