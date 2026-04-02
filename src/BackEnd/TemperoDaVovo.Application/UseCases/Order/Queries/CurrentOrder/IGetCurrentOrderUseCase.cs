using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;

public interface IGetCurrentOrderUseCase
{
    Task<OrderDetailResponseJson?> ExecuteAsync(Guid restaurantId, string clientSessionId);

}