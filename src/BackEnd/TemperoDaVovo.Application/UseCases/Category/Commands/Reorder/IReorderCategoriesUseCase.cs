using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Reorder;

public interface IReorderCategoriesUseCase
{
    Task<ReorderCategoriesResponseJson> ExecuteAsync(ReorderCategoriesRequest request);
}