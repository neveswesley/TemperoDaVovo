namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteGroup;

public interface IDeleteSideDishGroupUseCase
{
    Task Execute(Guid groupId);
}