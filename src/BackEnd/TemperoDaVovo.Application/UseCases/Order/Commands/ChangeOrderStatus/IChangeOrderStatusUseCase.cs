namespace TemperoDaVovo.Application.UseCases.Order.Commands.AcceptOrder;

public interface IChangeOrderStatusUseCase
{
    Task<Guid> ExecuteAsync(Guid orderId);
}