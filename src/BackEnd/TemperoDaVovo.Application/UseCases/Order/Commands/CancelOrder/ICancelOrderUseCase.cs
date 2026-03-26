using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancelOrder;

public interface ICancelOrderUseCase
{
    Task ExecuteAsync(Guid orderId, CancelOrderByRestaurantRequestJson request);
}