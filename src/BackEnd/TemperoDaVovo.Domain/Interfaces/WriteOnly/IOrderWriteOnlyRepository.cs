using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IOrderWriteOnlyRepository
{
    Task<Guid> Create(Order order);
    Task<Guid> Update(Order order);
    Task AddItemToExistingOrder(OrderItem item);
    Task UpdateOrderItem(OrderItem orderItem, CancellationToken ct = default);
}