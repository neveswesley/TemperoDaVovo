using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;

public class CompleteCheckoutUseCase : ICompleteCheckoutUseCase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;

    public CompleteCheckoutUseCase(IUnitOfWork unitOfWork, IOrderReadOnlyRepository orderReadOnlyRepository)
    {
        _unitOfWork = unitOfWork;
        _orderReadOnlyRepository = orderReadOnlyRepository;
    }

    public async Task ExecuteAsync(CompleteCheckoutRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOrderById(request.OrderId);
        if (order is null)
            throw new NotFoundException(["Pedido não encontrado."]);

        var name = await _orderReadOnlyRepository.ExistingPhone(request.Phone);
        
        order.CompleteCheckout(name ?? request.Name, request.Phone);
        
        await _unitOfWork.CommitAsync();
    }
}