using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;

public interface ICancelOrderRequestUseCase
{
    Task<Guid> ExecuteAsync(Guid orderId, CancelOrderByCustomerRequestJson byCustomerRequest);
}