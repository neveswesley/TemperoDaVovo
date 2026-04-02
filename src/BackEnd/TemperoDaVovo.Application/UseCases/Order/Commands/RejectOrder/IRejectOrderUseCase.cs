using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.RejectOrder;

public interface IRejectOrderUseCase
{
    Task ExecuteAsync(Guid orderId, RejectOrderRequestJson request);
}