using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IOrderWriteOnlyRepository
{
    Task<Guid> Create(Order order);
    Task<Guid> Update(Order order);
}