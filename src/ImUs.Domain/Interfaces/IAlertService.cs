using ImUs.Domain.Entities;

namespace ImUs.Domain.Interfaces;

public interface IAlertService
{
    Task<IEnumerable<Alert>> GetRecentAlertsAsync(int storeId, int count = 20);
    Task<Alert> CreateAlertAsync(Alert alert);
    Task MarkAsReadAsync(int alertId);
}
