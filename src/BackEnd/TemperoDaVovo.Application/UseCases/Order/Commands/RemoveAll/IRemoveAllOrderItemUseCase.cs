namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;

public interface IRemoveAllOrderItemUseCase
{
    Task ExecuteAsync(Guid orderId);
}