namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;

public interface IRemoveOrderItemUseCase
{
    Task Execute(Guid orderItemId);
}