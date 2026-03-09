using Microsoft.AspNetCore.Http.Timeouts;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Finalize;

public class FinalizeOrderUseCase : IFinalizeOrderUseCase
{
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly INeighborhoodReadOnlyRepository _neighborhoodReadOnlyRepository;
    private readonly IPaymentWriteOnlyRepository _paymentWriteOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FinalizeOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository,
        IOrderReadOnlyRepository orderReadOnlyRepository,
        INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository,
        IPaymentWriteOnlyRepository paymentWriteOnlyRepository,
        IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _paymentWriteOnlyRepository = paymentWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(CheckoutOrderRequestJson request)
    {
        if (request is { PaymentWay: PaymentWay.Cash, AmountGiven: null })
            throw new BusinessException(["Valor em dinheiro é obrigatório."]);

        var order = await _orderReadOnlyRepository.GetOrderById(request.OrderId);
        if (order == null)
            throw new NotFoundException(["Pedido não encontrado."]);

        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(order.RestaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        var neighborhood = await _neighborhoodReadOnlyRepository.GetNeighborhoodById(request.NeighborhoodId);
        if (neighborhood == null && request.DeliveryMode == DeliveryMode.Delivery)
            throw new NotFoundException(["Bairro não encontdado"]);

        

        if (request.DeliveryMode == DeliveryMode.Delivery)
        {
            order.OrderAddress(request.Street, request.Number, request.Complement, request.Reference,
                request.NeighborhoodId, request.City, request.AddressName);
            var estimatedTime = neighborhood.BaseDeliveryTimeInMinutes + restaurant.GlobalAdditionalDeliveryMinutes;
        
            order.SetDeliveryFee(request.DeliveryFee);
            order.SetEstimatedDeliveryTimeInMinutes(estimatedTime);
        }
        else if (request.DeliveryMode == DeliveryMode.Pickup)
        {
            order.DeliveryMode = DeliveryMode.Pickup;
            order.DeliveryFee = 0;
            order.SetEstimatedDeliveryTimeInMinutes(20);
        }

        var payment = new Domain.Entities.Payment(order.Id, request.PaymentWay, order.Total);

        if (request.PaymentWay == PaymentWay.Cash)
        {
            payment.ProcessCash(request.AmountGiven.Value);
            order.SetPayment(payment.Id);
        }
        else if (request.PaymentWay == PaymentWay.Pix)
        {
            payment.MarkAsPaidManually();
            order.SetPayment(payment.Id);
        }
        else if (request.PaymentWay == PaymentWay.Card)
        {
            payment.ProcessCard();
            order.SetPayment(payment.Id);
        }
        
        order.UpdateStatus(OrderStatus.PendingConfirmation);

        await _orderWriteOnlyRepository.Update(order);
        await _paymentWriteOnlyRepository.CreateAsync(payment);
        await _unitOfWork.CommitAsync();
        return order.Id;
    }
}