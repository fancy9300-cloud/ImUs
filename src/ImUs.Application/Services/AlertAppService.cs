using ImUs.Application.DTOs;
using ImUs.Domain.Entities;
using ImUs.Domain.Interfaces;

namespace ImUs.Application.Services;

public class AlertAppService : IAlertService
{
    private readonly IRepository<Alert> _alertRepo;

    public AlertAppService(IRepository<Alert> alertRepo)
    {
        _alertRepo = alertRepo;
    }

    public async Task<IEnumerable<Alert>> GetRecentAlertsAsync(int storeId, int count = 20)
    {
        var alerts = await _alertRepo.FindAsync(a => a.StoreId == storeId);
        return alerts.OrderByDescending(a => a.CreatedAt).Take(count);
    }

    public async Task<Alert> CreateAlertAsync(Alert alert)
    {
        await _alertRepo.AddAsync(alert);
        await _alertRepo.SaveChangesAsync();
        return alert;
    }

    public async Task MarkAsReadAsync(int alertId)
    {
        var alert = await _alertRepo.GetByIdAsync(alertId);
        if (alert is null) return;
        alert.IsRead = true;
        _alertRepo.Update(alert);
        await _alertRepo.SaveChangesAsync();
    }

    public IEnumerable<AlertDto> MapToDtos(IEnumerable<Alert> alerts)
    {
        return alerts.Select(a => new AlertDto
        {
            Id = a.Id,
            Type = a.Type,
            Severity = a.Severity,
            Message = a.Message,
            IsRead = a.IsRead,
            CreatedAt = a.CreatedAt,
            DeviceName = a.Device?.Name
        });
    }
}
