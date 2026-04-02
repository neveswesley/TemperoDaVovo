using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.AbandonOrder;

public class AbandonOrderUseCase : IAbandonOrderUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public AbandonOrderUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid orderId)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Order not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(order.RestaurantId);

        if (order.Status != Domain.Enums.OrderStatus.Draft)
            return;

        order.Abandon();

        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();
    }
}