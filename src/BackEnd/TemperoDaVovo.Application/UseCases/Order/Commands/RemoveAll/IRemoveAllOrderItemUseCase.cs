namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;

public interface IRemoveAllOrderItemUseCase
{
    Task Execute(Guid orderId);
}