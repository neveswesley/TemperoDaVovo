using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.SideDish.Queries.GetSideDishGroupsByProduct;

public interface IGetAllSideDishGroupsByProduct
{
    Task<List<GetAllSideDishGroupsResponse>> Execute(Guid productId);
}