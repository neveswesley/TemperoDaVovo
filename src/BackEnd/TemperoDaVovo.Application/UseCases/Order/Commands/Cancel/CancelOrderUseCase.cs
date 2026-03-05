using TemperoDaVovo.Application.Interfaces;
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

    public CancelOrderUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork, ICurrentUser currentUser)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
        _currentUser = currentUser;
    }

    public async Task<Guid> ExecuteAsync(CancelOrderRequestJson request)
    {
        // pega o pedido
        var order = await _orderReadOnlyRepository.GetOrderById(request.OrderId);
        if (order is null)
            throw new NotFoundException(["Pedido não encontrado."]);
        
        
        // verifica se o lado que tá cancelando, pode cancelar esse pedido
        if (_currentUser.IsAuthenticated)
        {
            var restaurantId = _currentUser.RestaurantId;

            if (order.RestaurantId != restaurantId)
                throw new UnauthorizedException(["Você não pode cancelar esse pedido."]);

            order.Cancel(request.Reason, CanceledBy.Restaurant, request.Description);
        }
        else
        {
            if (order.ClientSessionId != request.ClientSessionId)
                throw new UnauthorizedException(["Você não pode cancelar esse pedido."]);

            order.Cancel(request.Reason, CanceledBy.Customer, request.Description);
        }

        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();

        return order.Id;

    }
}