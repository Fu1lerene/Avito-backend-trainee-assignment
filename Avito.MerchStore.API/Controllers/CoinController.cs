using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avito.MerchStore.API.Controllers;

[Route("api/sendCoin")]
[ApiController]
public class CoinController : ControllerBase
{
    private readonly ICoinService _coinService;

    public CoinController(ICoinService coinService)
    {
        _coinService = coinService;
            
    }

    /// <summary>
    /// Отправить монеты другому пользователю.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> SendCoins([FromBody] SendCoinRequest request)
    {
        var sender = User.Identity.Name;
        var response = await _coinService.SendCoins(sender, request.ToUser, request.Amount);
        
        if (response is not null)
        {
            return BadRequest(response.ErrorMessage);
        }
        return Ok();
    }
}