using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Application.UseCases.Order.Commands.RejectOrder;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancelByRestaurant;

public class RejectOrderUseCase : IRejectOrderUseCase
{
    
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;
    private readonly IAuthorizationService _authorizationService;

    public RejectOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository, IOrderReadOnlyRepository orderReadOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier, IAuthorizationService authorizationService)
    {
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid orderId, RejectOrderRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Order not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);
        
        order.RejectOrder(request.CancellationReasonType, request.CancellationDescription);
        await _orderWriteOnlyRepository.Update(order);
        
        await _unitOfWork.CommitAsync();
        
        await _orderNotifier.NotifyOrderUpdated(order.RestaurantId, new
        {
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
            cancellationRequestStatus = order.CancellationRequestStatus
        });
        
        await _orderNotifier.NotifyCustomerOrderUpdated(order.ClientSessionId, new
        {
            id = order.Id,
            status = order.Status,
            cancellationRequestStatus = order.CancellationRequestStatus
        });
        
    }
}