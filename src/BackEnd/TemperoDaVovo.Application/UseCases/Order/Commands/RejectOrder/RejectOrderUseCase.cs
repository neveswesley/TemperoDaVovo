using TemperoDaVovo.Application.Services;
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

    public RejectOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository, IOrderReadOnlyRepository orderReadOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier)
    {
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
    }

    public async Task ExecuteAsync(Guid orderId, RejectOrderRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Order not found."]);
        
        order.RejectOrder(request.CancellationReasonType, request.CancellationDescription);
        await _orderWriteOnlyRepository.Update(order);
        
        await _orderNotifier.NotifyOrderUpdated(order.RestaurantId, new
        {
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
            cancellationRequestStatus = order.CancellationRequestStatus
        });
        
        await _unitOfWork.CommitAsync();
        
    }
}