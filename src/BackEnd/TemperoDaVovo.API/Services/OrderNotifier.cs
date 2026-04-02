using Microsoft.AspNetCore.SignalR;
using TemperoDaVovo.API.Hubs;
using TemperoDaVovo.Application.Services;

namespace TemperoDaVovo.API.Services;

public class OrderNotifier : IOrderNotifier
{
    private readonly IHubContext<OrdersHub> _hubContext;

    public OrderNotifier(IHubContext<OrdersHub> hubContext)
    {
        _hubContext = hubContext;
    }

    public async Task NotifyOrderCreated(Guid restaurantId, object payload)
    {
        await _hubContext.Clients
            .Group($"restaurant-{restaurantId}")
            .SendAsync("OrderCreated", payload);
    }

    public async Task NotifyOrderUpdated(Guid restaurantId, object payload)
    {
        await _hubContext.Clients
            .Group($"restaurant-{restaurantId}")
            .SendAsync("OrderUpdated", payload);
    }

    public async Task NotifyCustomerOrderUpdated(string clientSessionId, object payload)
    {
        await _hubContext.Clients
            .Group($"customer-{clientSessionId}")
            .SendAsync("OrderUpdated", payload);
    }
}