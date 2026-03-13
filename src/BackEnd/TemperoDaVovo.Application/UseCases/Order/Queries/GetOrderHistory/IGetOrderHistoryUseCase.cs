using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Common;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderHistory;

public interface IGetOrderHistoryUseCase
{
    Task<PaginatedResponse<OrderHistoryResponseJson>> ExecuteAsync(
        Guid restaurantId,
        int page,
        int pageSize);
}