using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CancelOrder;

public class CancelOrderUseCase : ICancelOrderUseCase
{

    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;
    private readonly ICurrentUser _currentUser;
    private readonly IAuthorizationService _authorizationService;

    public CancelOrderUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier, ICurrentUser currentUser, IAuthorizationService authorizationService)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
        _currentUser = currentUser;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid orderId, CancelOrderByRestaurantRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Order not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);
        
        order.Cancel(request.Reason, request.CanceledBy, request.CancellationDescription);
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