using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.ApproveCancellationRequest;

public class ApproveCancellationRequestUseCase : IApproveCancellationRequestUseCase
{

    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;
    private readonly IAuthorizationService _authorizationService;

    public ApproveCancellationRequestUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier, IAuthorizationService authorizationService)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid orderId, ApproveCancellationRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Pedido não encontrado."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);
        
        order.ApproveCancellationRequest(request.CancellationReasonType, request.CancellationReason);
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