using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface ICityReadOnlyRepository
{
    Task<City> GetByIdAsync(Guid cityId);
    Task<List<City>> GetAll(Guid restaurantId);
    Task<bool> CityExistingByRestaurantId(string cityName, Guid restaurantId);
}