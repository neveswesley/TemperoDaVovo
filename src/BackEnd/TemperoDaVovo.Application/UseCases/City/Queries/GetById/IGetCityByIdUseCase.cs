using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.City.Queries.GetById;

public interface IGetCityByIdUseCase
{
    Task<CityResponseJson> ExecuteAsync(Guid cityId);
}