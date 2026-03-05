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

    public async Task<List<GetOrderByClientResponse>> Execute(string clientId)
    {
        
        var client = await _orderReadOnlyRepository.ExistingClient(clientId);
        if (client == null)
            throw new NotFoundException(["Cliente não encontrado."]);

        var orders = await _orderReadOnlyRepository.GetOrdersByClienteId(clientId);
        if (orders is null)
            throw new NotFoundException(["Nenhum pedido encontrado."]);

        return orders.Select(o => new GetOrderByClientResponse
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
            Complement = o.Complement,
            EstimatedDeliveryTimeInMinutes = o.EstimatedDeliveryTimeInMinutes,
            PaymentWay = o.Payment?.PaymentWay,
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