using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Create;

public interface ICreateCityUseCase
{
    Task<Guid> ExecuteAsync(CreateCityRequestJson request);
}