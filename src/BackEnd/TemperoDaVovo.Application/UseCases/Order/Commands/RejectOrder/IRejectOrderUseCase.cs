using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancelByRestaurant;

public interface IRejectOrderUseCase
{
    Task ExecuteAsync(Guid orderId, RejectOrderRequestJson request);
}