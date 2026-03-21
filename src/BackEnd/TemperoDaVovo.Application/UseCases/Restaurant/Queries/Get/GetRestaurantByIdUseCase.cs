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

        return new RestaurantResponse
        {
            Name = restaurant.Name,
            Phone = restaurant.Phone,
            Description = restaurant.Description,
            RestaurantCategory = restaurant.RestaurantCategory.HasValue
                ? (int)restaurant.RestaurantCategory.Value
                : null,
            Address = restaurant.Address == null ? null : new AddressResponse
            {
                Street = restaurant.Address.Street,
                Number = restaurant.Address.Number,
                Neighborhood = restaurant.Address.Neighborhood,
                City = restaurant.Address.City,
                State = restaurant.Address.State,
                ZipCode = restaurant.Address.ZipCode,
                Complement = restaurant.Address.Complement,
            },
            PaymentWays = restaurant.PaymentWays,
            OpeningHours = restaurant.OpeningHours.Select(h => new OpeningHourResponse
            {
                DayOfWeek = h.DayOfWeek,
                OpenTime = h.OpenTime.ToString("HH:mm:ss"),
                CloseTime = h.CloseTime.ToString("HH:mm:ss"),
            }).ToList(),
        };
    }
}