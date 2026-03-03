using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface INeighborhoodWriteOnlyRepository
{
    Task<Neighborhood> AddAsync(Neighborhood neighborhood);
}