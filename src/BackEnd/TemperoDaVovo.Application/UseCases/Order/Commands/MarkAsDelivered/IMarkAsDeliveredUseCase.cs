namespace TemperoDaVovo.Application.UseCases.Order.Commands.MarkAsDelivered;

public interface IMarkAsDeliveredUseCase
{
    Task<Guid> ExecuteAsync(Guid orderId);
}