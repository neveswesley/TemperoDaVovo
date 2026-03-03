using TemperoDaVovo.Application.UseCases.City.Queries.GetById;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.City.Queries.GetAll;

public interface IGetAllCitiesByRestaurantId
{
    Task<List<CityResponseJson>>  ExecuteAsync(Guid restaurantId);
}