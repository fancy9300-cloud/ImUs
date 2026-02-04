using ImUs.Domain.Enums;

namespace ImUs.Domain.Entities;

public class Alert
{
    public int Id { get; set; }
    public AlertType Type { get; set; }
    public Severity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
    public int? DeviceId { get; set; }
    public Device? Device { get; set; }
}
