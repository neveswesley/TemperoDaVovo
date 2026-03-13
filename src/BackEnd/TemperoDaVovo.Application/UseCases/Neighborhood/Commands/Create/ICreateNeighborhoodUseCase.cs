using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;

public interface ICreateNeighborhoodUseCase
{
    Task<Guid> ExecuteAsync(CreateNeighborhoodRequestJson request);
}