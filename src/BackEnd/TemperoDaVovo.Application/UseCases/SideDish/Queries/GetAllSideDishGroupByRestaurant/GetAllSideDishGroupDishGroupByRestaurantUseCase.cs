using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllProductSideDish;

public class GetAllSideDishGroupDishGroupByRestaurantUseCase : IGetAllSideDishGroupByRestaurant0UseCase
{
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;

    public GetAllSideDishGroupDishGroupByRestaurantUseCase(ISideDishReadOnlyRepository sideDishReadOnlyRepository)
    {
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
    }


    public async Task<List<GetAllSideDishGroupsResponse>> Execute(Guid restauranteId)
    {
        var sideDish = await _sideDishReadOnlyRepository.GetAllSideDishesByRestaurant(restauranteId);
        var response = sideDish.Select(s => new GetAllSideDishGroupsResponse()
            {
                Id = s.Id,
                RestaurantId = s.RestaurantId,
                Name = s.Name,
                SideDish = s.SideDish.Select(d => new SideDishResponseJson
                {
                    Id = d.Id,
                    SideDishGroupId = d.SideDishGroupId,
                    Name = d.Name,
                    Quantity = d.Quantity,
                    UnitPrice = d.UnitPrice
                }).ToList(),
                MinQuantity = s.MinQuantity,
                MaxQuantity = s.MaxQuantity
            }
        ).ToList();

        return response;
    }
}