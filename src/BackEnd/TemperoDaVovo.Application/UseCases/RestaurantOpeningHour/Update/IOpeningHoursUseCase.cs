using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.OpeningHours;

public interface IOpeningHoursUseCase
{
    Task Execute(Guid restaurantId, UpdateRestaurantOpeningHoursRequest request);
}