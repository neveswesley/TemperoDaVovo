using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;

public class GetCurrentOrderUseCase : IGetCurrentOrderUseCase
{
    private readonly IOrderReadOnlyRepository _orderRepository;

    public GetCurrentOrderUseCase(IOrderReadOnlyRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDetailResponseJson?> Execute(Guid restaurantId, string clientSessionId)
    {
        var order = await _orderRepository.GetOpenBySession(restaurantId, clientSessionId);
        if (order == null) return null;

        return new OrderDetailResponseJson
        {
            Id = order.Id,
            RestaurantId = order.RestaurantId,
            ClientSessionId = order.ClientSessionId,
            OrderNumber = order.OrderNumber,
            SubTotal = order.SubTotal,
            Total = order.Total,
            DeliveryFee = order.DeliveryFee,
            Status = order.Status.ToString(),
            DeliveryMode = order.DeliveryMode.ToString(),
            Street = order.Street,
            Number = order.Number,
            Complement = order.Complement,
            Neighborhood = order.Neighborhood?.Name,
            City = order.City,
            PaymentWay = order.Payment?.PaymentWay.ToString(),
            EstimatedDeliveryTimeInMinutes = order.EstimatedDeliveryTimeInMinutes,
            CreatedAt = order.CreatedAt,
            PreparingStartedAt = order.PreparingStartedAt,
            OnTheWayAt = order.OnTheWayAt,
            ReadyAt = order.ReadyAt,
            CanceledAt = order.CanceledAt,
            Items = order.Items.Select(i => new OrderItemResponseJson
            {
                Id = i.Id,
                ProductId = i.OriginalProductId ?? Guid.Empty,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                Observation = i.Observation,
                Total = i.TotalPrice,
                SideDishes = i.SideDishes.Select(sd => new OrderSideDishResponseJson
                {
                    SideDishId = sd.OriginalSideDishId ?? Guid.Empty,
                    Name = sd.Name,
                    UnitPrice = sd.UnitPrice,
                    Quantity = sd.Quantity
                }).ToList()
            }).ToList()
        };
    }
}