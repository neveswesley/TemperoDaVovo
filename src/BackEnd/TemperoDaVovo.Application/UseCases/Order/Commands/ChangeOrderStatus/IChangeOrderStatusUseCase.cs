namespace TemperoDaVovo.Application.UseCases.Order.Commands.ChangeOrderStatus;

public interface IChangeOrderStatusUseCase
{
    Task<Guid> ExecuteAsync(Guid orderId);
}