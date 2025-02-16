using Avito.MerchStore.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Avito.MerchStore.API.Controllers;

[Route("api/e2e")]
[ApiController]
public class E2ETestsController : ControllerBase
{
    private readonly IMerchStoreRepository _merchStoreRepository;

    public E2ETestsController(IMerchStoreRepository merchStoreRepository)
    {
        _merchStoreRepository = merchStoreRepository;
    }
    
    [Route("user/{username}")]
    [HttpGet]
    public async Task<IActionResult> GetUserByName(string username)
    {
        var user = await _merchStoreRepository.GetUserByName(username);
        
        return Ok(user);
    }
    
    [Route("merch/{itemname}")]
    [HttpGet]
    public async Task<IActionResult> GetItemByName(string itemname)
    {
        var item = await _merchStoreRepository.GetItemByName(itemname);
        
        return Ok(item);
    }
}