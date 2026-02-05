namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDish;

public interface IDeleteSideDishUseCase
{
    Task Execute(Guid sideDishId);
}