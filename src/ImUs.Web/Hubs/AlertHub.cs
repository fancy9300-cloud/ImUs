using Microsoft.AspNetCore.SignalR;

namespace ImUs.Web.Hubs;

public class AlertHub : Hub
{
    public async Task JoinStoreGroup(string storeId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"store-{storeId}");
    }

    public async Task LeaveStoreGroup(string storeId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"store-{storeId}");
    }
}
