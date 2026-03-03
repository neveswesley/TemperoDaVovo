using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IPaymentWriteOnlyRepository
{
    Task<Guid> CreateAsync(Payment payment);
}