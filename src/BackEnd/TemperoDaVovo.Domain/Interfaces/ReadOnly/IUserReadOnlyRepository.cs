namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IUserReadOnlyRepository
{
    Task<bool> EmailExists(string email);
    Task<bool> RestaurantHasUser(Guid restaurantId);
}