using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Queries.GetAllSideDishGroups;

public interface IGetAllSideDishGroupsUseCase
{
    Task<List<GetAllSideDishGroupsResponse>> Execute(Guid restaurantId);
}