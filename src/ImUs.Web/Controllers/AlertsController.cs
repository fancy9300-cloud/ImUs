using ImUs.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImUs.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertsController : ControllerBase
{
    private readonly AlertAppService _alertService;

    public AlertsController(AlertAppService alertService)
    {
        _alertService = alertService;
    }

    [HttpGet]
    public async Task<IActionResult> GetByStore([FromQuery] int storeId, [FromQuery] int count = 20)
    {
        var alerts = await _alertService.GetRecentAlertsAsync(storeId, count);
        return Ok(_alertService.MapToDtos(alerts));
    }

    [HttpPost("{id}/read")]
    public async Task<IActionResult> MarkAsRead(int id)
    {
        await _alertService.MarkAsReadAsync(id);
        return NoContent();
    }
}
