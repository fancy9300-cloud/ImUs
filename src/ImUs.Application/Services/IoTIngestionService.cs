using ImUs.Application.DTOs;
using ImUs.Domain.Entities;
using ImUs.Domain.Enums;
using ImUs.Domain.Interfaces;

namespace ImUs.Application.Services;

public class IoTIngestionService
{
    private readonly IRepository<SensorReading> _readingRepo;
    private readonly IRepository<Device> _deviceRepo;
    private readonly IAlertService _alertService;

    private static readonly Dictionary<string, double> Thresholds = new()
    {
        { "temperature", 8.0 },
        { "vibration", 50.0 },
        { "humidity", 80.0 }
    };

    public IoTIngestionService(
        IRepository<SensorReading> readingRepo,
        IRepository<Device> deviceRepo,
        IAlertService alertService)
    {
        _readingRepo = readingRepo;
        _deviceRepo = deviceRepo;
        _alertService = alertService;
    }

    public async Task<Alert?> IngestAsync(SensorReadingDto dto)
    {
        var device = await _deviceRepo.GetByIdAsync(dto.DeviceId);
        if (device is null) return null;

        var reading = new SensorReading
        {
            DeviceId = dto.DeviceId,
            MetricName = dto.MetricName,
            Value = dto.Value,
            Unit = dto.Unit,
            Timestamp = DateTime.UtcNow
        };

        await _readingRepo.AddAsync(reading);

        device.IsOnline = true;
        device.LastSeen = DateTime.UtcNow;
        _deviceRepo.Update(device);

        await _readingRepo.SaveChangesAsync();

        if (Thresholds.TryGetValue(dto.MetricName.ToLowerInvariant(), out var threshold)
            && dto.Value > threshold)
        {
            var alert = new Alert
            {
                Type = AlertType.TemperatureThreshold,
                Severity = dto.Value > threshold * 1.5 ? Severity.Critical : Severity.Warning,
                Message = $"{dto.MetricName} = {dto.Value}{dto.Unit} on {device.Name} exceeds threshold ({threshold}{dto.Unit})",
                StoreId = device.StoreId,
                DeviceId = device.Id
            };

            return await _alertService.CreateAlertAsync(alert);
        }

        return null;
    }
}
