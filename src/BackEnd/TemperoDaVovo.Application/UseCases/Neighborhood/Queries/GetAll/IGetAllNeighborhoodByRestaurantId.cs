using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Queries.GetAll;

public interface IGetAllNeighborhoodByRestaurantId
{
    Task<List<NeighborhoodResponseJson>> Execute (Guid restaurantId);
}