using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface INeighborhoodReadOnlyRepository
{
    Task<bool> ExistingNameByCity(string name, Guid  cityId);
    Task<List<Neighborhood>> GetNeighborhoodByRestaurantId(Guid restaurantId);
}