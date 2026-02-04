namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.RemoveSideDishGroup;

public interface IRemoveSideDishGroupUseCase
{
    Task Execute(Guid productId, List<Guid> sideDishGroupIds);
}