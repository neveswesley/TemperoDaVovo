using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.ChangeOrderStatus;

public class ChangeOrderStatusUseCase : IChangeOrderStatusUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;
    private readonly IAuthorizationService _authorizationService;

    public ChangeOrderStatusUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier, IAuthorizationService authorizationService)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
        _authorizationService = authorizationService;
    }

    public async Task<Guid> ExecuteAsync(Guid orderId)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Pedido não encontrado."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);

        if (order.Status == OrderStatus.PendingConfirmation)
        {
            order.ChangeOrderStatus(OrderStatus.Preparing);
            order.SetPreparingStartedAt(DateTime.UtcNow);
        }
            
        else if (order.Status == OrderStatus.Preparing)
        {
            order.ChangeOrderStatus(OrderStatus.OnTheWay);
            order.SetOnTheWayAt(DateTime.UtcNow);
        }

        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();

        await _orderNotifier.NotifyOrderUpdated(order.RestaurantId, new
        {
            id = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
        });

        await _orderNotifier.NotifyCustomerOrderUpdated(order.ClientSessionId, new
        {
            id = order.Id,
            status = order.Status,
            cancellationRequestStatus = order.CancellationRequestStatus
        });

        return order.Id;
    }
}