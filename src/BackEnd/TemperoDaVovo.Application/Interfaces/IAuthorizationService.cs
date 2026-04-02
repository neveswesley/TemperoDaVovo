namespace TemperoDaVovo.Application.Interfaces;

public interface IAuthorizationService
{
    void ValidateRestaurantOwnership(Guid restaurantId);
    void ValidateUserOwnership(Guid userId);
}