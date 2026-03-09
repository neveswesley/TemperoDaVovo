using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Queries.GetAll;

public class GetAllCitiesByRestaurantId : IGetAllCitiesByRestaurantId
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;

    public GetAllCitiesByRestaurantId(ICityReadOnlyRepository cityReadOnlyRepository,
        IRestaurantReadOnlyRepository restaurantReadOnlyRepository)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
    }

    public async Task<List<CityResponseJson>> ExecuteAsync(Guid restaurantId)
    {
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(restaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        var cities = await _cityReadOnlyRepository.GetAll(restaurantId);
        if (cities == null)
            throw new NotFoundException(["Nenhuma cidade cadastrada neste restaurante."]);

        return cities.Select(c => new CityResponseJson()
        {
            Id = c.Id,
            Name = c.Name,
            Neighborhoods = c.Neighborhoods.Select(n => new NeighborhoodResponseJson()
            {
                Id = n.Id,
                Name = n.Name,
                DeliveryFee = n.DeliveryFee,
                City = n.City.Name,
                EstimatedDeliveryTimeInMinutes = n.BaseDeliveryTimeInMinutes
            }).ToList()
        }).ToList();
    }
}