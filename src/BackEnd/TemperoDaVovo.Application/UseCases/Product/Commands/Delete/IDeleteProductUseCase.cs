namespace TemperoDaVovo.Application.UseCases.Product.Commands.Delete;

public interface IDeleteProductUseCase
{
    Task ExecuteAsync(Guid productId);
}