using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Get;

public interface IGetOpeningHoursUseCase
{
    Task<List<OpeningHourResponse>> ExecuteAsync(Guid restaurantId);
}