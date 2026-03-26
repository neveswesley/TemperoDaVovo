using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.Services;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;

public class CancelOrderRequestUseCase : ICancelOrderRequestUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderNotifier _orderNotifier;

    public CancelOrderRequestUseCase(
        IOrderReadOnlyRepository orderReadOnlyRepository,
        IOrderWriteOnlyRepository orderWriteOnlyRepository,
        IUnitOfWork unitOfWork,
        IOrderNotifier orderNotifier)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _orderNotifier = orderNotifier;
    }

    public async Task<Guid> ExecuteAsync(Guid orderId, CancelOrderByCustomerRequestJson byCustomerRequest)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order is null)
            throw new NotFoundException(["Pedido não encontrado."]);

        if (order.ClientSessionId != byCustomerRequest.ClientSessionId)
            throw new UnauthorizedException(["Você não pode solicitar o cancelamento desse pedido."]);

        if (string.IsNullOrWhiteSpace(byCustomerRequest.Description))
            throw new BusinessException(["Informe o motivo da solicitação de cancelamento."]);

        order.RequestCancellation(byCustomerRequest.Description.Trim());

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