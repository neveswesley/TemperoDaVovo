using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;

public class UpdateOrderItemUseCase : IUpdateOrderItemUseCase
{
    
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateOrderItemUseCase(IOrderReadOnlyRepository orderReadOnlyRepository, IOrderWriteOnlyRepository orderWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(Guid orderItemId, UpdateOrderItemRequest request, CancellationToken ct = default)
    {
        await Validate(request);

        var orderItem = await _orderReadOnlyRepository.GetByIdWithSideDishesAsync(orderItemId, ct);
        if (orderItem is null)
            throw new NotFoundException(["O item não foi encontrado."]);

        // Build new side dishes snapshot
        var newSideDishes = new List<OrderItemSideDish>();
        foreach (var sdDto in request.SideDishes)
        {
            var sideDish = await _sideDishReadOnlyRepository.GetSideDishById(sdDto.SideDishId);
            if (sideDish is null)
                throw new NotFoundException(["O complemento não foi encontrado."]);

            newSideDishes.Add(OrderItemSideDish.Create(
                orderItemId:        orderItemId,
                originalSideDishId: sideDish.Id,
                name:               sideDish.Name,
                unitPrice:          sideDish.UnitPrice,
                quantity:           sdDto.Quantity
            ));
        }

        orderItem.Update(
            quantity:    request.Quantity,
            observation: request.Observation,
            sideDishes:  newSideDishes
        );

        await _orderWriteOnlyRepository.UpdateOrderItem(orderItem, ct);
        await _unitOfWork.CommitAsync();
        return orderItem.Id;
    }

    private async Task Validate(UpdateOrderItemRequest request)
    {
        var validator = new UpdateOrderItemValidator();
        var result = await validator.ValidateAsync(request);


        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}