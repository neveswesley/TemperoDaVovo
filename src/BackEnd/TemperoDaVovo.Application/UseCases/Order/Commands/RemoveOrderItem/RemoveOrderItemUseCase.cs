using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;

public class RemoveOrderItemUseCase : IRemoveOrderItemUseCase
{
    
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveOrderItemUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid orderItemId)
    {
        var orderItem = await _orderReadOnlyRepository.GetOrderItemById(orderItemId);
        if (orderItem == null)
            throw new NotFoundException(["Item do produto não encontrado."]);

        var order = await _orderReadOnlyRepository.GetOrderById(orderItem.OrderId);
        if (order == null)
            throw new NotFoundException(["Pedido não encontrado."]);

        order.RemoveItemAndRecalculate(orderItemId);

        await _orderWriteOnlyRepository.RemoveItemByCart(orderItemId);
    
        await _unitOfWork.CommitAsync();
    }
}