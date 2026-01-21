namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IRestaurantReadOnlyRepository
{
    Task<bool> PhoneExists(string phone);
    Task<bool> RestaurantExists(Guid restaurantId);
}