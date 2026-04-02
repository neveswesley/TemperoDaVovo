using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Update;

public interface IOpeningHoursUseCase
{
    Task ExecuteAsync(Guid restaurantId, UpdateRestaurantOpeningHoursRequest request);
}