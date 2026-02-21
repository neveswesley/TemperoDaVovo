using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;

public interface IGetCurrentOrderUseCase
{
    Task<OrderDetailResponseJson?> Execute(Guid restaurantId, string clientSessionId);

}