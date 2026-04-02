using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Commands.Update;

public class UpdateRestaurantUseCase : IUpdateRestaurantUseCase
{
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public UpdateRestaurantUseCase(IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync (Guid restaurantId, UpdateRestaurantRequest request)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(restaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurant not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(restaurantId);

        var address = new Address(request.Address.ZipCode,
            request.Address.State,
            request.Address.City,
            request.Address.Neighborhood,
            request.Address.Street,
            request.Address.Number,
            request.Address.Complement);
        
        restaurant.UpdateName(request.Name);
        restaurant.UpdateDescription(request.Description);
        restaurant.UpdateRestaurantCategory(request.RestaurantCategory);
        restaurant.UpdateAddress(address);

        await _unitOfWork.CommitAsync();
    }
}