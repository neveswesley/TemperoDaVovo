using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;

public interface IUpdateOrderItemUseCase
{
    Task<Guid> Execute(Guid orderItemId, UpdateOrderItemRequest request, CancellationToken ct = default);
}