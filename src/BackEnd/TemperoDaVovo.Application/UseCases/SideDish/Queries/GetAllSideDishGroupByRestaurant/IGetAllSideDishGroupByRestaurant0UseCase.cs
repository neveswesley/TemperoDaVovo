using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllProductSideDish;

public interface IGetAllSideDishGroupByRestaurant0UseCase
{
    Task<List<GetAllSideDishGroupsResponse>> Execute(Guid restauranteId);
}