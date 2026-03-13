using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface INeighborhoodWriteOnlyRepository
{
    Task<Neighborhood> AddAsync(Neighborhood neighborhood);
    void UpdateAsync(Neighborhood neighborhood);
    void DeleteAsync(Neighborhood neighborhood);
}