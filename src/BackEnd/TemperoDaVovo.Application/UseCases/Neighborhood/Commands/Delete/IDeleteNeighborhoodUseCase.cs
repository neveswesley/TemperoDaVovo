namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Update;

public interface IDeleteNeighborhoodUseCase
{
    Task ExecuteAsync(Guid neighborhoodId);
}