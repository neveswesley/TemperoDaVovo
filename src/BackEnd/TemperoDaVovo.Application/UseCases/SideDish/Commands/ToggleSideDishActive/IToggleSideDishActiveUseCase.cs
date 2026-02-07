using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.ToggleSideDishActive;

public interface IToggleSideDishActiveUseCase
{
    Task<ToggleSideDishActiveResponseJson> Execute(Guid sideDishId, bool isActive);
}