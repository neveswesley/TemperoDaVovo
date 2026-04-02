using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;

public interface ICompleteCheckoutUseCase
{
    Task ExecuteAsync(CompleteCheckoutRequestJson request);
}