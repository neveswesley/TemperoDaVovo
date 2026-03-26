using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancellationRequest;

public interface IApproveCancellationRequestUseCase
{
    Task ExecuteAsync(Guid orderId, ApproveCancellationRequestJson request);
}