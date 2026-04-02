namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;

public interface IRemoveOrderItemUseCase
{
    Task ExecuteAsync(Guid orderItemId);
}