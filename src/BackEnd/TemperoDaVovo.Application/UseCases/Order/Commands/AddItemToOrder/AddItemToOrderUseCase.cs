using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Domain.Services;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;

public class AddItemToOrderUseCase : IAddItemToOrderUseCase
{
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IRestaurantScheduleService _restaurantScheduleService;
    private readonly IUnitOfWork _unitOfWork;

    public AddItemToOrderUseCase(IOrderWriteOnlyRepository orderWriteOnlyRepository, IOrderReadOnlyRepository orderReadOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IProductReadOnlyRepository productReadOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IRestaurantScheduleService restaurantScheduleService, IUnitOfWork unitOfWork)
    {
        _orderWriteOnlyRepository = orderWriteOnlyRepository;
        _orderReadOnlyRepository = orderReadOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _productReadOnlyRepository = productReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _restaurantScheduleService = restaurantScheduleService;
        _unitOfWork = unitOfWork;
    }

    public async Task<OrderResponseJson> Execute(AddItemToOrderRequestJson request)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetByIdWithOpeningHours(request.RestaurantId);

        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        var isOpenNow = _restaurantScheduleService.IsOpenNow(restaurant.OpeningHours, DateTime.Now);

        if (!isOpenNow)
            throw new BusinessException(["O restaurante está fechado no momento."]);
        
        var order = await _orderReadOnlyRepository
            .GetOpenBySession(request.RestaurantId, request.ClientSessionId);

        var isNew = order == null;

        if (isNew)
            order = new Domain.Entities.Order(
                request.RestaurantId, 
                request.ClientSessionId, 
                "", ""
            );

        var product = await _productReadOnlyRepository
            .GetProductByIdWithCategory(request.ProductId);

        if (product == null)
            throw new BusinessException(["Produto não existe"]);

        var item = new OrderItem(
            order.Id,
            product.Id,
            product.Name,
            product.Price,
            request.Quantity,
            request.Observation
        );

        foreach (var sd in request.SideDishes)
        {
            var sideDish = await _sideDishReadOnlyRepository
                .GetSideDishById(sd.SideDishId);

            if (sideDish is null)
                throw new BusinessException(["Acompanhamento inválido"]);

            var groupName = sideDish.SideDishGroup?.Name ?? string.Empty;

            item.AddSideDish(
                new OrderItemSideDish(
                    sideDish.Id,
                    sideDish.Name,
                    groupName,
                    sideDish.UnitPrice,
                    sd.Quantity
                )
            );
        }

        item.Recalculate();
        order.AddItem(item);
        order.CalculateTotals();

        if (isNew)
        {
            var nextNumber = await _orderWriteOnlyRepository.GetNextOrderNumber();
            order.SetOrderNumber(nextNumber);
            await _orderWriteOnlyRepository.Create(order);
        }
        else
        {
            await _orderWriteOnlyRepository.AddItemToExistingOrder(item);
        }

        await _unitOfWork.CommitAsync();

        return new OrderResponseJson(
            order.Id,
            order.SubTotal,
            order.Total,
            order.Items.Count
        );
    }
}