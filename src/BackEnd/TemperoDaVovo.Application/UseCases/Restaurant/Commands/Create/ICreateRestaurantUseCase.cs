using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.Create;

public interface ICreateRestaurantUseCase
{
    Task<RestaurantResponseJson> ExecuteAsync(CreateRestaurantRequestJson request);
}