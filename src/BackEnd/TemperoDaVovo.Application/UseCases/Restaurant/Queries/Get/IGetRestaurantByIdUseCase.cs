using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Queries.Get;

public interface IGetRestaurantByIdUseCase
{
    Task<RestaurantResponse> ExecuteAsync(Guid restaurantId);
}