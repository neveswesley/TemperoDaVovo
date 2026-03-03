using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ICityWriteOnlyRepository
{
    Task<Guid> CreateAsync(City city);
    Task DeleteAsync(City city);
    Task<Guid> UpdateAsync(City city);
}