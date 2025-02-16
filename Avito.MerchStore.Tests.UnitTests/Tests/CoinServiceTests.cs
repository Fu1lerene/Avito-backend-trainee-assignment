using Avito.MerchStore.API.Services;
using Avito.MerchStore.Domain.Repositories;
using Avito.MerchStore.Domain.Repositories.Models;
using Avito.MerchStore.Domain.Services;
using FluentAssertions;
using Moq;

namespace Avito.MerchStore.Tests.UnitTests.Tests;

public class CoinServiceTests
{
    private readonly Mock<IMerchStoreRepository> _repositoryMock;
    private readonly ICoinService _coinService;
    private readonly IUserService _userService;

    public CoinServiceTests()
    {
        _repositoryMock = new Mock<IMerchStoreRepository>();
        _userService = new UserService(_repositoryMock.Object);
        _coinService = new CoinService(_userService, _repositoryMock.Object);
    }
    
    [Fact]
    public async Task SendCoins_CorrectData_ShouldReturnNull()
    {
        var senderUsername = "Test";
        var receiverUsername = "Aboba";
        var sender = new User { Username = senderUsername, Balance = 1000 };
        var receiver = new User { Username = receiverUsername, Balance = 1000 };

        _repositoryMock.Setup(x => x.GetUserByName(senderUsername)).ReturnsAsync(sender);
        _repositoryMock.Setup(x => x.GetUserByName(receiverUsername)).ReturnsAsync(receiver);
        
        var response = await _coinService.SendCoins(senderUsername, receiverUsername, 10);

        response?.ErrorMessage.Should().BeNull();
        _repositoryMock.Verify(x => x.SendCoins(
            It.IsAny<User>(),
            It.IsAny<User>(), 
            It.IsAny<int>()),
            Times.Once);
    }

    [Fact]
    public async Task SendCoins_NotEnoughCoins_ShouldReturnError()
    {
        var senderUsername = "Test";
        var receiverUsername = "Aboba";
        var sender = new User { Username = senderUsername, Balance = 1 };
        var receiver = new User { Username = receiverUsername, Balance = 1000 };

        _repositoryMock.Setup(x => x.GetUserByName(senderUsername)).ReturnsAsync(sender);
        _repositoryMock.Setup(x => x.GetUserByName(receiverUsername)).ReturnsAsync(receiver);
        
        var response = await _coinService.SendCoins(senderUsername, receiverUsername, 100);

        response?.ErrorMessage.Should().Be("Недостаточно монет для перевода");
    }
    
    [Fact]
    public async Task SendCoins_SendToYourself_ShouldReturnError()
    {
        var senderUsername = "Test";
        var receiverUsername = "Test";
        var sender = new User { Username = senderUsername, Balance = 1000 };
        var receiver = new User { Username = receiverUsername, Balance = 1000 };

        _repositoryMock.Setup(x => x.GetUserByName(senderUsername)).ReturnsAsync(sender);
        _repositoryMock.Setup(x => x.GetUserByName(receiverUsername)).ReturnsAsync(receiver);
        
        var response = await _coinService.SendCoins(senderUsername, receiverUsername, 10);

        response?.ErrorMessage.Should().Be("Некорректный получатель");
    }
    
    [Fact]
    public async Task SendCoins_NotExistReceiver_ShouldReturnError()
    {
        var senderUsername = "Test";
        var receiverUsername = "NotExist";
        var sender = new User { Username = senderUsername, Balance = 1000 };

        _repositoryMock.Setup(x => x.GetUserByName(senderUsername)).ReturnsAsync(sender);
        _repositoryMock.Setup(x => x.GetUserByName(receiverUsername)).ReturnsAsync(null as User);
        
        var response = await _coinService.SendCoins(senderUsername, receiverUsername, 10);

        response?.ErrorMessage.Should().Be("Некорректный получатель");
    }
    
    [Fact]
    public async Task SendCoins_NonPositiveSendCoins_ShouldReturnError()
    {
        var senderUsername = "Test";
        var receiverUsername = "Aboba";
        
        var response = await _coinService.SendCoins(senderUsername, receiverUsername, -100);

        response?.ErrorMessage.Should().Be("Минимальное количество для передачи монет: 1");
    }
}