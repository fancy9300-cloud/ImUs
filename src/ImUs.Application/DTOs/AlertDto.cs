using ImUs.Domain.Enums;

namespace ImUs.Application.DTOs;

public class AlertDto
{
    public int Id { get; set; }
    public AlertType Type { get; set; }
    public Severity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? DeviceName { get; set; }
}
