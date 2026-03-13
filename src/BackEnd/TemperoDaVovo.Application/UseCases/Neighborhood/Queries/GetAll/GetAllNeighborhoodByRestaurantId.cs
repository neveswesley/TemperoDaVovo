using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Queries.GetAll;

public class GetAllNeighborhoodByRestaurantId : IGetAllNeighborhoodByRestaurantId
{
    
    private readonly INeighborhoodReadOnlyRepository _neighborhoodReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;

    public GetAllNeighborhoodByRestaurantId(INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository)
    {
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
    }

    public async Task<List<NeighborhoodResponseJson>> Execute(Guid restaurantId)
    {
       var neighborhood = await _neighborhoodReadOnlyRepository.GetNeighborhoodByRestaurantId(restaurantId);
        if (neighborhood.Count == 0)
            throw new NotFoundException(["Nenhum bairro encontrado para esta cidade."]);
        
        return neighborhood.Select(n=>new NeighborhoodResponseJson()
        {
            Id = n.Id,
            Name = n.Name,
            City = n.City.Name,
            DeliveryFee = n.DeliveryFee,
            BaseDeliveryTimeInMinutes = n.BaseDeliveryTimeInMinutes
        }).ToList();
    }
}