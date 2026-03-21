using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.UpdatePaymentWay;

public class UpdatePaymentWayUseCase : IUpdatePaymentWayUseCase
{
    
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IRestaurantWriteOnlyRepository _restaurantWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentWayUseCase(IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IRestaurantWriteOnlyRepository restaurantWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _restaurantWriteOnlyRepository = restaurantWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid restaurantId, SetPaymentWayRequest request)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(restaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurant not found."]);
        
        restaurant.SetPaymentWay(request.PaymentWays);
        
        _restaurantWriteOnlyRepository.Update(restaurant);
        await _unitOfWork.CommitAsync();
    }
}