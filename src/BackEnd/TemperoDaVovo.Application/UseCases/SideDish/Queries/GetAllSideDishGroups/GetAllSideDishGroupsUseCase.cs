using TemperoDaVovo.Application.UseCases.SideDishGroup.Queries.GetAllSideDishGroups;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllSideDishGroups;

public class GetAllSideDishGroupsUseCase : IGetAllSideDishGroupsUseCase
{
    
    private readonly ISideDishReadOnlyRepository _repository;

    public GetAllSideDishGroupsUseCase(ISideDishReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<GetAllSideDishGroupsResponse>> Execute(Guid restaurantId)
    {
        var sideDishes = await _repository.GetAllSideDishesByRestaurant(restaurantId);

        var response = sideDishes.Select(s => new GetAllSideDishGroupsResponse
        {
            RestaurantId = s.RestaurantId,
            Name = s.Name,
            MinQuantity = s.MinQuantity,
            MaxQuantity = s.MaxQuantity,
            SideDish = s.SideDish.Select(d=>new SideDishResponseJson
            {
                Id = d.Id,
                Name = d.Name,
                Quantity = d.Quantity,
                UnitPrice = d.UnitPrice
            }).ToList()
        }).ToList();

        return response;

    }
}