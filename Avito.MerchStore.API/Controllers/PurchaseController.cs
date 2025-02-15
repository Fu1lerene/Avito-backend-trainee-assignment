using Avito.MerchStore.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avito.MerchStore.API.Controllers;

[Route("api/buy")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    /// <summary>
    /// Купить предмет за монеты.
    /// </summary>
    /// <param name="item">Название предмета</param>
    /// <param name="quantity">Количество предметов (по умолчанию 1)</param>
    /// <returns></returns>
    [HttpGet("{item}")]
    [Authorize]
    public async Task<IActionResult> BuyItem(string item, int quantity = 1)
    {
        var username = User.Identity.Name;
        var response = await _purchaseService.BuyItem(username, item, quantity);
        
        if (response is not null)
        {
            return BadRequest(response.ErrorMessage);
        }
        return Ok();
    }
}