using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;

public interface ICancelOrderUseCase
{
    Task<Guid> ExecuteAsync(Guid orderId, CancelOrderRequestJson request);
}