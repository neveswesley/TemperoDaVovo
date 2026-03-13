using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Common;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IOrderReadOnlyRepository
{
    Task<Order?> GetOpenBySession(Guid restaurantId, string sessionId);
    Task<OrderItem> GetOrderItemById(Guid orderItemId);
    Task<OrderItem?> GetTrackedById(Guid id);
    Task<OrderItem?> GetByIdWithSideDishesAsync(Guid orderItemId, CancellationToken ct = default);
    Task<Order?> GetOrderById(Guid orderId);
    Task<string?> ExistingPhone(string phone);
    Task<List<Order>> GetOrdersByClientId(string sessionId);
    Task<List<Order>> GetActiveOrdersByRestaurantId(Guid restaurantId);
    Task<PaginatedResponse<Order>> GetOrderHistoryByRestaurantId(
        Guid restaurantId,
        int page,
        int pageSize);
    Task<bool> ExistingClient(string sessionId);
    
}