using ImUs.Domain.Enums;

namespace ImUs.Domain.Entities;

public class Device
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DeviceType Type { get; set; }
    public string Manufacturer { get; set; } = string.Empty;
    public string SerialNumber { get; set; } = string.Empty;
    public bool IsOnline { get; set; }
    public DateTime LastSeen { get; set; }
    public int StoreId { get; set; }
    public Store Store { get; set; } = null!;
    public int? ZoneId { get; set; }
    public Zone? Zone { get; set; }

    public ICollection<SensorReading> Readings { get; set; } = new List<SensorReading>();
}
