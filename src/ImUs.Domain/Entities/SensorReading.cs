namespace ImUs.Domain.Entities;

public class SensorReading
{
    public int Id { get; set; }
    public string MetricName { get; set; } = string.Empty;
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public int DeviceId { get; set; }
    public Device Device { get; set; } = null!;
}
