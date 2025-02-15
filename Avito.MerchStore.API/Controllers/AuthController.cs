using Avito.MerchStore.Domain.Models;
using Avito.MerchStore.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Avito.MerchStore.API.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    /// <summary>
    /// Аутентификация и получение JWT-токена
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<AuthResponse>> Authenticate([FromBody] AuthRequest request)
    {
        var token = await _authService.Authenticate(request.Username, request.Password);
        if (token == null)
        {
            return Unauthorized();
        }
        
        return Ok(new AuthResponse
        {
            Token = token
        });
    }
}