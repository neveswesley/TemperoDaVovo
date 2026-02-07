using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Duplicate;

public interface IDuplicateProductUseCase
{
    Task<DuplicateProductResponseJson> ExecuteAsync(DuplicateProductRequestJson request);
}