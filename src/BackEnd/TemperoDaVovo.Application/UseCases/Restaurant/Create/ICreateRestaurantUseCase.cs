using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Create;

public interface ICreateRestaurantUseCase
{
    Task<CreateRestaurantResponseJson> Execute(CreateRestaurantRequestJson request);
}