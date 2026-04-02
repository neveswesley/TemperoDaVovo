using TemperoDaVovo.Communications.Requests;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.ApproveCancellationRequest;

public interface IApproveCancellationRequestUseCase
{
    Task ExecuteAsync(Guid orderId, ApproveCancellationRequestJson request);
}