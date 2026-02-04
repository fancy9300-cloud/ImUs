using ImUs.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImUs.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoresController : ControllerBase
{
    private readonly StoreService _storeService;

    public StoresController(StoreService storeService)
    {
        _storeService = storeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var stores = await _storeService.GetAllStoresAsync();
        return Ok(stores);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var store = await _storeService.GetStoreAsync(id);
        if (store is null) return NotFound();
        return Ok(store);
    }
}
