using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Restaurant.Queries.Get;

public class GetRestaurantByIdUseCase : IGetRestaurantByIdUseCase
{
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;

    public GetRestaurantByIdUseCase(IRestaurantReadOnlyRepository restaurantReadOnlyRepository)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
    }

    public async Task<RestaurantResponse> ExecuteAsync(Guid restaurantId)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetByIdWithOpeningHours(restaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurant not found."]);

        return new RestaurantResponse()
        {
            Name = restaurant.Name,
            Phone = restaurant.Phone,
            Description = restaurant.Description,
            Address = restaurant.Address,
            RestaurantCategory = restaurant.RestaurantCategory
        };
    }
}