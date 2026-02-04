using ImUs.Application.DTOs;
using ImUs.Domain.Entities;
using ImUs.Domain.Interfaces;

namespace ImUs.Application.Services;

public class StoreService
{
    private readonly IRepository<Store> _storeRepo;

    public StoreService(IRepository<Store> storeRepo)
    {
        _storeRepo = storeRepo;
    }

    public async Task<IEnumerable<StoreDto>> GetAllStoresAsync()
    {
        var stores = await _storeRepo.GetAllAsync();
        return stores.Select(s => new StoreDto
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            City = s.City,
            TenantName = s.Tenant?.Name ?? "",
            DeviceCount = s.Devices?.Count ?? 0,
            ZoneCount = s.Zones?.Count ?? 0
        });
    }

    public async Task<StoreDto?> GetStoreAsync(int id)
    {
        var s = await _storeRepo.GetByIdAsync(id);
        if (s is null) return null;
        return new StoreDto
        {
            Id = s.Id,
            Name = s.Name,
            Address = s.Address,
            City = s.City,
            TenantName = s.Tenant?.Name ?? "",
            DeviceCount = s.Devices?.Count ?? 0,
            ZoneCount = s.Zones?.Count ?? 0
        };
    }
}
