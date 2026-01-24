using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Commands;

public interface ICreateCategoryUseCase
{
    Task<CreateCategoryResponseJson> Execute(CreateCategoryRequestJson request, Guid restaurantId);
}