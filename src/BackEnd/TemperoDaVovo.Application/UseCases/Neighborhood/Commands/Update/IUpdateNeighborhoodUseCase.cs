using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Delete;

public interface IUpdateNeighborhoodUseCase
{
    Task ExecuteAsync (Guid neighborhoodId, UpdateNeighborhoodRequestJson request);
}