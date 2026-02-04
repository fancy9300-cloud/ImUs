namespace ImUs.Application.DTOs;

public class SensorReadingDto
{
    public int DeviceId { get; set; }
    public string MetricName { get; set; } = string.Empty;
    public double Value { get; set; }
    public string Unit { get; set; } = string.Empty;
}
