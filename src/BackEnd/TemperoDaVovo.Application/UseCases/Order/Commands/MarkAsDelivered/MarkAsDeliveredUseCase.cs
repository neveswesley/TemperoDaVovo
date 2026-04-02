using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.MarkAsDelivered;

public class MarkAsDeliveredUseCase : IMarkAsDeliveredUseCase
{

    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;
    private readonly IAuthorizationService _authorizationService;

    public MarkAsDeliveredUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier, IAuthorizationService authorizationService)
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
            throw new NotFoundException(["Order not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);
        
        order.MarkAsDelivered();
        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();
        
        await _orderNotifier.NotifyOrderUpdated(order.RestaurantId, new
        {
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
        });
        
        return order.Id;
        
    }
}