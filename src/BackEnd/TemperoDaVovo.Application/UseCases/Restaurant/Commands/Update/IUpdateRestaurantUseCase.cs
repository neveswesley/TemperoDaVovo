using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.Update;

public interface IUpdateRestaurantUseCase
{
    Task ExecuteAsync (Guid restaurantId, UpdateRestaurantRequest request);
}