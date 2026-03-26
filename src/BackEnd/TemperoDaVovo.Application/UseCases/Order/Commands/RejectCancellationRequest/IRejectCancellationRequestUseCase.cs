using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancellationRequest;

public interface IRejectCancellationRequestUseCase
{
    Task ExecuteAsync(Guid orderId, RejectCancellationRequestJson request);
}