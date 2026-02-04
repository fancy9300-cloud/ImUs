using ImUs.Application.DTOs;
using ImUs.Domain.Entities;
using ImUs.Domain.Interfaces;

namespace ImUs.Application.Services;

public class DeviceAppService : IDeviceService
{
    private readonly IRepository<Device> _deviceRepo;

    public DeviceAppService(IRepository<Device> deviceRepo)
    {
        _deviceRepo = deviceRepo;
    }

    public async Task<IEnumerable<Device>> GetDevicesByStoreAsync(int storeId)
    {
        return await _deviceRepo.FindAsync(d => d.StoreId == storeId);
    }

    public async Task<Device?> GetDeviceAsync(int id)
    {
        return await _deviceRepo.GetByIdAsync(id);
    }

    public async Task UpdateDeviceStatusAsync(int deviceId, bool isOnline)
    {
        var device = await _deviceRepo.GetByIdAsync(deviceId);
        if (device is null) return;
        device.IsOnline = isOnline;
        device.LastSeen = DateTime.UtcNow;
        _deviceRepo.Update(device);
        await _deviceRepo.SaveChangesAsync();
    }

    public IEnumerable<DeviceDto> MapToDtos(IEnumerable<Device> devices)
    {
        return devices.Select(d => new DeviceDto
        {
            Id = d.Id,
            Name = d.Name,
            Type = d.Type,
            Manufacturer = d.Manufacturer,
            SerialNumber = d.SerialNumber,
            IsOnline = d.IsOnline,
            LastSeen = d.LastSeen,
            ZoneName = d.Zone?.Name
        });
    }
}
