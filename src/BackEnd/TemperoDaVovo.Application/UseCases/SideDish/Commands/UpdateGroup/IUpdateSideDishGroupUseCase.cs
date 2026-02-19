using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDishGroup;

public interface IUpdateSideDishGroupUseCase
{
    Task<Guid> Execute(UpdateSideDishGroupJson request, Guid sideDishGroupId);
}