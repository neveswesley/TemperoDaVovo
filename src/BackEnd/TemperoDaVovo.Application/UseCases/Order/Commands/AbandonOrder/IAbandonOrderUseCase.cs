namespace TemperoDaVovo.Application.UseCases.Order.Commands.AbandonOrder;

public interface IAbandonOrderUseCase
{
    Task ExecuteAsync(Guid orderId);
}
