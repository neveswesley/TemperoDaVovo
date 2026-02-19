namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDish;

public interface IUpdateSideDishUseCase
{
    Task Execute(Guid sideDishId, string name, int quantity, decimal price);
}