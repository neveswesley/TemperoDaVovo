using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.Update;

public interface IUpdateRestaurantUseCase
{
    Task<RestaurantResponse> ExecuteAsync (Guid restaurantId);
}