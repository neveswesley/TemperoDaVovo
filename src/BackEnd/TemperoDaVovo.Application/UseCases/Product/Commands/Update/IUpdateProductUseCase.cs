using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Update;

public interface IUpdateProductUseCase
{
    Task ExecuteAsync(UpdateProductRequestJson request, Guid productId);
}