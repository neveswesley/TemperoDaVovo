using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IRestaurantWriteOnlyRepository
{
    Task<Guid> AddAsync(Restaurant restaurant);
    Task<bool> ExistActiveRestaurantWithPhone(string phone);
}