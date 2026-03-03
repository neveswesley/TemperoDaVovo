using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Finalize;

public interface IFinalizeOrderUseCase
{
    Task<Guid> ExecuteAsync(CheckoutOrderRequestJson request);
}