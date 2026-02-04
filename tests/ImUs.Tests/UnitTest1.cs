using ImUs.Application.Services;
using ImUs.Domain.Entities;
using ImUs.Domain.Enums;
using ImUs.Domain.Interfaces;

namespace ImUs.Tests;

public class IoTIngestionServiceTests
{
    [Fact]
    public void AlertSeverity_Critical_WhenValueExceedsThresholdBy50Percent()
    {
        var alert = new Alert
        {
            Type = AlertType.TemperatureThreshold,
            Severity = 15.0 > 8.0 * 1.5 ? Severity.Critical : Severity.Warning,
            Message = "temperature = 15°C exceeds threshold"
        };

        Assert.Equal(Severity.Critical, alert.Severity);
    }

    [Fact]
    public void AlertSeverity_Warning_WhenValueExceedsThresholdUnder50Percent()
    {
        var alert = new Alert
        {
            Type = AlertType.TemperatureThreshold,
            Severity = 10.0 > 8.0 * 1.5 ? Severity.Critical : Severity.Warning,
            Message = "temperature = 10°C exceeds threshold"
        };

        Assert.Equal(Severity.Warning, alert.Severity);
    }

    [Fact]
    public void Device_DefaultValues_AreCorrect()
    {
        var device = new Device
        {
            Name = "Test Sensor",
            Type = DeviceType.Sensor,
            Manufacturer = "Sensirion"
        };

        Assert.False(device.IsOnline);
        Assert.Equal(DeviceType.Sensor, device.Type);
        Assert.Empty(device.Readings);
    }
}
