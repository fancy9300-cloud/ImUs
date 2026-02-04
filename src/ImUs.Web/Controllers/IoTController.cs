using ImUs.Application.DTOs;
using ImUs.Application.Services;
using ImUs.Web.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ImUs.Web.Controllers;

[ApiController]
[Route("api/iot")]
public class IoTController : ControllerBase
{
    private readonly IoTIngestionService _ingestionService;
    private readonly IHubContext<AlertHub> _hubContext;

    public IoTController(IoTIngestionService ingestionService, IHubContext<AlertHub> hubContext)
    {
        _ingestionService = ingestionService;
        _hubContext = hubContext;
    }

    [HttpPost("ingest")]
    public async Task<IActionResult> Ingest([FromBody] SensorReadingDto dto)
    {
        var alert = await _ingestionService.IngestAsync(dto);

        if (alert is not null)
        {
            await _hubContext.Clients.Group($"store-{alert.StoreId}")
                .SendAsync("NewAlert", new
                {
                    alert.Id,
                    Type = alert.Type.ToString(),
                    Severity = alert.Severity.ToString(),
                    alert.Message,
                    alert.CreatedAt
                });

            return Ok(new { reading = "saved", alert = alert.Message });
        }

        return Ok(new { reading = "saved", alert = (string?)null });
    }
}
