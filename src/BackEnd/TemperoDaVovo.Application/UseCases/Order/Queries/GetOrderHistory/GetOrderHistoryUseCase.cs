using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Common;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderHistory;

public class GetOrderHistoryUseCase : IGetOrderHistoryUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

    public GetOrderHistoryUseCase(IOrderReadOnlyRepository orderReadOnlyRepository)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
    }

    public async Task<PaginatedResponse<OrderHistoryResponseJson>> ExecuteAsync(
        Guid restaurantId,
        int page,
        int pageSize)
    {
        var result = await _orderReadOnlyRepository.GetOrderHistoryByRestaurantId(restaurantId, page, pageSize);

        return new PaginatedResponse<OrderHistoryResponseJson>
        {
            Page = result.Page,
            PageSize = result.PageSize,
            TotalItems = result.TotalItems,
            TotalPages = result.TotalPages,
            Data = result.Data.Select(o => new OrderHistoryResponseJson
            {
                OrderNumber = o.OrderNumber,
                CreatedAt = o.CreatedAt,
                Total = o.Total,
                CustomerName = o.CustomerName,
                CustomerPhone = o.CustomerPhone,
                Status = o.Status,
                DeliveryMode = o.DeliveryMode,
                Payment = o.Payment == null
                    ? null
                    : new PaymentResponseJson
                    {
                        PaymentWay = o.Payment.PaymentWay,
                        Status = o.Payment.Status,
                        Total = o.Payment.Total,
                        AmountPaid = o.Payment.AmountPaid,
                        Change = o.Payment.Change,
                    },
                Items = o.Items.Select(i => new OrderItemResponseJson
                {
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Total = i.TotalPrice,
                    Observation = i.Observation,
                    SideDishes = i.SideDishes.Select(s => new OrderSideDishResponseJson
                    {
                        Name = s.Name,
                        GroupName = s.GroupName,
                        Quantity = s.Quantity,
                        UnitPrice = s.UnitPrice,
                    }).ToList()
                }).ToList()
            }).ToList()
        };
    }
}