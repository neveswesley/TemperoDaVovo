using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.UpdatePaymentWay;

public interface IUpdatePaymentWayUseCase
{
    Task ExecuteAsync(Guid restaurantId, SetPaymentWayRequest request);
}