namespace TemperoDaVovo.Application.UseCases.Product.Commands.RemoveImage;

public interface IRemoveProductImageUseCase
{
    Task ExecuteAsync(Guid productId);
}