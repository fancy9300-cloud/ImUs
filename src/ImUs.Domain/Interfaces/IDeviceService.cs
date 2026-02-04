using ImUs.Domain.Entities;

namespace ImUs.Domain.Interfaces;

public interface IDeviceService
{
    Task<IEnumerable<Device>> GetDevicesByStoreAsync(int storeId);
    Task<Device?> GetDeviceAsync(int id);
    Task UpdateDeviceStatusAsync(int deviceId, bool isOnline);
}
