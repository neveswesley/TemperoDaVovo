using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByRestaurant;

public class GetOrderByRestaurantId : IGetOrderByRestaurantId
{

    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

    public GetOrderByRestaurantId(IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IOrderReadOnlyRepository orderReadOnlyRepository)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
    }

    public async Task<List<GetOrderResponse>> ExecuteAsync(Guid restaurantId)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(restaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontado."]);

        var orders = await _orderReadOnlyRepository.GetOrdersByRestaurantId(restaurantId);
        
        if (orders == null)
            throw new NotFoundException(["Nenhum pedido encontrado."]);

        return orders.Select(o => new GetOrderResponse
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
            CustomerName = o.CustomerName,
            CustomerPhone = o.CustomerPhone,
            CreatedAt = o.CreatedAt,
            Status = o.Status,
            SubTotal = o.SubTotal,
            DeliveryFee = o.DeliveryFee,
            Total = o.Total,
            DeliveryMode = o.DeliveryMode,
            Street = o.Street,
            Number = o.Number,
            Neighborhood = o.Neighborhood?.Name,
            City = o.City,
            Complement = o.Complement,
            Reference = o.Reference,
            EstimatedDeliveryTimeInMinutes = o.EstimatedDeliveryTimeInMinutes,
            PendingConfirmationAt = o.PendingConfirmationAt,
            Payment = o.Payment == null ? null : new PaymentResponseJson
            {
                PaymentWay = o.Payment.PaymentWay,
                Status = o.Payment.Status,
                Total = o.Payment.Total,
                AmountPaid = o.Payment.AmountPaid,
                Change = o.Payment.Change,
            },
            PreparingStartedAt = o.PreparingStartedAt,
            OnTheWayAt = o.OnTheWayAt,
            ReadyAt = o.ReadyAt,
            CanceledAt = o.CanceledAt,
            Items = o.Items.Select(i => new OrderItemResponseJson()
            {
                Id = i.Id,
                ProductId = i.OriginalProductId,
                ProductName = i.ProductName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity,
                Observation = i.Observation,
                Total = i.TotalPrice,
                SideDishes = i.SideDishes.Select(sd => new OrderSideDishResponseJson
                {
                    SideDishId = sd.Id,
                    Name = sd.Name,
                    UnitPrice = sd.UnitPrice,
                    Quantity = sd.Quantity
                }).ToList()
            }).ToList()
        }).ToList();
    }
}