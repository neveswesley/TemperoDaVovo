using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ICityWriteOnlyRepository
{
    Task<Guid> CreateAsync(City city);
    void DeleteAsync(City city);
    void UpdateAsync(City city);
}