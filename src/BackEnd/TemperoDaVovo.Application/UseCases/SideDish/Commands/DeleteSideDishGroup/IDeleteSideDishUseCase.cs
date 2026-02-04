namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDishGroup;

public interface IDeleteSideDishUseCase
{
    Task Execute(Guid sideDishGroupId);
}