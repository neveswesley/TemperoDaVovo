using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IRestaurantWriteOnlyRepository
{
    Task<Guid> AddAsync(Restaurant restaurant);
    void Update(Restaurant restaurant);
    void RemoveOpeningHours(List<RestaurantOpeningHour> hours);
}