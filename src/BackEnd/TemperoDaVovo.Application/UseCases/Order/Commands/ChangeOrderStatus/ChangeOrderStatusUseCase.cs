using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Application.UseCases.Order.Commands.AcceptOrder;
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


    public ChangeOrderStatusUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IOrderNotifier orderNotifier)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
    }

    public async Task<Guid> ExecuteAsync(Guid orderId)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Pedido não encontrado."]);

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
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
        });

        return order.Id;
    }
}