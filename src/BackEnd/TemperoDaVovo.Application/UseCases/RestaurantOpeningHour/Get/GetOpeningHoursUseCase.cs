using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.RestaurantOpeningHour.Get;

public class GetOpeningHoursUseCase : IGetOpeningHoursUseCase
{
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;

    public GetOpeningHoursUseCase(IRestaurantReadOnlyRepository restaurantReadOnlyRepository)
    {
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
    }


    public async Task<List<OpeningHourResponse>> ExecuteAsync(Guid restaurantId)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetByIdWithOpeningHours(restaurantId);

        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        return restaurant.OpeningHours.Select(x => new OpeningHourResponse
        {
            DayOfWeek = x.DayOfWeek,
            OpenTime = x.OpenTime.ToString(@"hh\:mm\:ss"),
            CloseTime = x.CloseTime.ToString(@"hh\:mm\:ss"),
        }).ToList();
    }
}