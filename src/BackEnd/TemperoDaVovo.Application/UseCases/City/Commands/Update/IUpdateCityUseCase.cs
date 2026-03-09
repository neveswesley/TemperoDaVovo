using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Update;

public interface IUpdateCityUseCase
{
    Task<Guid> ExecuteAsync(Guid cityId, UpdateCityRequestJson request);
}