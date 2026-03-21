using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace TemperoDaVovo.API.Hubs;

[Authorize]
public class OrdersHub : Hub
{
    public async Task JoinRestaurantGroup(string restaurantId)
    {
        Console.WriteLine($"[SignalR] ConnectionId {Context.ConnectionId} entrou no grupo restaurant-{restaurantId}");
        await Groups.AddToGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }

    public async Task LeaveRestaurantGroup(string restaurantId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"restaurant-{restaurantId}");
    }
}