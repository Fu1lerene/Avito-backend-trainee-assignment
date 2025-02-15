using Avito.MerchStore.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Avito.MerchStore.API.Controllers;

[Route("api/info")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly IUserService _userService;

    public InfoController(IUserService userService)
    {
        _userService = userService;
    }
    
    /// <summary>
    /// Получить информацию о монетах, инвентаре и истории транзакций.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetInfo()
    {
        var username = User.Identity.Name;
        var info = await _userService.GetInfo(username);
        
        return Ok(info);
    }
}