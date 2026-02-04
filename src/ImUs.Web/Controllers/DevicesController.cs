using ImUs.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImUs.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DevicesController : ControllerBase
{
    private readonly DeviceAppService _deviceService;

    public DevicesController(DeviceAppService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByStore([FromQuery] int storeId)
    {
        var devices = await _deviceService.GetDevicesByStoreAsync(storeId);
        return Ok(_deviceService.MapToDtos(devices));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var device = await _deviceService.GetDeviceAsync(id);
        if (device is null) return NotFound();
        return Ok(device);
    }
}
