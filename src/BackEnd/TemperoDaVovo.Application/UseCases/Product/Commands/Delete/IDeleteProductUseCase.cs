// Application/UseCases/Product/Commands/Delete/IDeleteProductUseCase.cs

namespace TemperoDaVovo.Application.UseCases.Product.Commands.Delete;

public interface IDeleteProductUseCase
{
    Task Execute(Guid productId);
}