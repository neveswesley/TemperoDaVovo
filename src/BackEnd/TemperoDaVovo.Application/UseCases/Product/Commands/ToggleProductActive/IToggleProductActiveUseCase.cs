using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;

public interface IToggleProductActiveUseCase
{
    Task<ToggleProductActiveResponseJson> Execute(Guid productId, bool isActive);
}