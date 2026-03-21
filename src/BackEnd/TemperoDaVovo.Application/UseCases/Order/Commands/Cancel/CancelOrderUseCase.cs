using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;
using TemperoDaVovo.Domain.Enums;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;

public class CancelOrderUseCase : ICancelOrderUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICurrentUser _currentUser;
    private readonly IOrderNotifier _orderNotifier;

    public CancelOrderUseCase(
        IOrderReadOnlyRepository orderReadOnlyRepository,
        IOrderWriteOnlyRepository orderWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        ICurrentUser currentUser,
        IOrderNotifier orderNotifier)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
        _orderNotifier = orderNotifier;
    }

    public async Task<Guid> ExecuteAsync(Guid orderId, CancelOrderRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order is null)
            throw new NotFoundException(["Pedido não encontrado."]);

        if (request.CanceledBy == CanceledBy.Restaurant)
        {
            order.Cancel(request.Reason, CanceledBy.Restaurant, request.Description);
        }
        else
        {
            if (order.ClientSessionId != request.ClientSessionId)
                throw new UnauthorizedException(["Você não pode cancelar esse pedido."]);

            if (string.IsNullOrWhiteSpace(request.Description))
                throw new BusinessException(["Informe o motivo da solicitação de cancelamento."]);

            order.RequestCancellation(request.Description);
        }

        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();

        await _orderNotifier.NotifyOrderUpdated(order.RestaurantId, new
        {
            orderId = order.Id,
            orderNumber = order.OrderNumber,
            status = order.Status,
            cancellationRequestStatus = order.CancellationRequestStatus
        });

        return order.Id;
    }
}