using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.RejectCancellationRequest;

public interface IRejectCancellationRequestUseCase
{
    Task ExecuteAsync(Guid orderId, RejectCancellationRequestJson request);
}