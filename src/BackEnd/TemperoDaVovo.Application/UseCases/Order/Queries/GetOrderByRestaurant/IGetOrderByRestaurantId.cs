using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByRestaurant;

public interface IGetOrderByRestaurantId
{
    Task<List<GetOrderResponse>> ExecuteAsync(Guid restaurantId);
}