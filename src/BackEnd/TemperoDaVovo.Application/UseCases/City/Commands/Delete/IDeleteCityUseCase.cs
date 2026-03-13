namespace TemperoDaVovo.Application.UseCases.City.Commands.Delete;

public interface IDeleteCityUseCase
{
    Task ExecuteAsync(Guid cityId);
}