using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IOrderReadOnlyRepository
{
    Task<Order?> GetOpenBySession(Guid restaurantId, string sessionId);
}