using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Update;

public interface IUpdateCategoryUseCase
{
 Task<UpdateCategoryResponseJson> ExecuteAsync(UpdateCategoryRequestJson request, Guid categodyId);   
}