using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByCliente;

public class GetOrderByClientUseCase : IGetOrderByClientUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

    public GetOrderByClientUseCase(IOrderReadOnlyRepository orderReadOnlyRepository)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
    }

    public async Task<List<GetOrderResponse>> Execute(string clientId)
    {
        
        var client = await _orderReadOnlyRepository.ExistingClient(clientId);
        if (client == null)
            throw new NotFoundException(["Cliente não encontrado."]);

        var orders = await _orderReadOnlyRepository.GetOrdersByClientId(clientId);
        if (orders is null)
            throw new NotFoundException(["Nenhum pedido encontrado."]);

        return orders.Select(o => new GetOrderResponse
        {
            Id = o.Id,
            OrderNumber = o.OrderNumber,
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