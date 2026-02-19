using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;

public class AddItemToOrderUseCase : IAddItemToOrderUseCase
{
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository,
        IOrderReadOnlyRepository orderReadOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository,
        IProductReadOnlyRepository productReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponseJson> Execute(AddItemToOrderRequestJson request)
    {
        var order = await _orderReadOnlyRepository.GetOpenBySession(request.RestaurantId, request.ClientSessionId) ??
                    new Domain.Entities.Order(request.RestaurantId, request.ClientSessionId, "", "");

        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(request.ProductId);

        if (product == null)
            throw new BusinessException(["Produto não existe"]);

        var item = new OrderItem(product.Id, product.Name, product.Price, request.Quantity, request.Observation);

        foreach (var sd in request.SideDishes)
        {
            var sideDish = await _sideDishReadOnlyRepository.GetSideDishById(sd.SideDishId);
            if (sideDish is null)
                throw new BusinessException(["Acompanhamento inválido"]);

            item.AddSideDish(new OrderItemSideDish(sideDish.Id, sideDish.Name, sideDish.UnitPrice, sd.Quantity));
        }

        item.Recalculate();
        order.AddItem(item);
        order.CalculateTotals();

        if (order.Id == Guid.Empty)
            await _orderWriteOnlyRepository.Create(order);

        else
            await _orderWriteOnlyRepository.Update(order);

        await _unitOfWork.CommitAsync();

        return new OrderResponseJson(
            order.Id, order.SubTotal, order.Total, order.Items.Count);
    }
}