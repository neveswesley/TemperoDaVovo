using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IOrderWriteOnlyRepository
{
    Task<Guid> Create(Order order);
    Task<Guid> Update(Order order);
    Task AddItemToExistingOrder(OrderItem item);
    Task UpdateOrderItem(OrderItem orderItem, CancellationToken ct = default);
    Task RemoveItemByCart(Guid orderItemId);
    Task<Guid> FinalizeOrder(Order order);
    Task<int> GetNextOrderNumber();
}