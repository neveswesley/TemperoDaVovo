using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.MarkAsDelivered;

public class MarkAsDeliveredUseCase : IMarkAsDeliveredUseCase
{

    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkAsDeliveredUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(Guid orderId)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(orderId);
        if (order == null)
            throw new NotFoundException(["Order not found."]);
        
        order.MarkAsDelivered();
        await _orderWriteOnlyRepository.Update(order);
        await _unitOfWork.CommitAsync();
        
        return order.Id;
        
    }
}