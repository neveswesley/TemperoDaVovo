using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;

public class RemoveAllOrderItemUseCase : IRemoveAllOrderItemUseCase
{

    private readonly IOrderWriteOnlyRepository _writeOnlyRepository;
    private readonly IOrderReadOnlyRepository _readOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveAllOrderItemUseCase(IOrderWriteOnlyRepository writeOnlyRepository, IOrderReadOnlyRepository readOnlyRepository, IUnitOfWork unitOfWork)
    {
        _writeOnlyRepository = writeOnlyRepository;
        _readOnlyRepository = readOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid orderId)
    {
        
        var order = await _readOnlyRepository.GetOrderById(orderId);
        if  (order is null)
            throw new NotFoundException(["Pedido não encontrado."]);
        
        order.RemoveAllOrderItems(orderId);
        order.CalculateTotals();
        await _unitOfWork.CommitAsync();
    }
}