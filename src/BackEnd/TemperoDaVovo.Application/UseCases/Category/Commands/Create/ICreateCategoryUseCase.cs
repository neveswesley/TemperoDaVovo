using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Create;

public interface ICreateCategoryUseCase
{
    Task<CreateCategoryResponseJson> ExecuteAsync(CreateCategoryRequestJson request);
}