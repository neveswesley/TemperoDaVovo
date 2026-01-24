using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.UpdateProduct;

public interface IUpdateCategoryUseCase
{
 Task<UpdateCategoryResponseJson> Execute(UpdateCategoryRequestJson request, Guid categodyId);   
}