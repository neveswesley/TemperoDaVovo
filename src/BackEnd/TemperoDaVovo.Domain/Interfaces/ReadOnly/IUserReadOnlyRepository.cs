using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IUserReadOnlyRepository
{
    Task<bool> EmailExists(string email);
    
    Task<bool> RestaurantHasAnyUser(Guid restaurantId);
    Task<User> GetByEmail(string email);
}