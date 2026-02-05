using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface ISideDishReadOnlyRepository
{
    Task<List<string>> GetExistingSideDishNames(Guid restaurantId, string name);
    Task<List<SideDishGroup>> GetAllSideDishesByRestaurant(Guid restaurantId);
    Task<SideDishGroup> GetSideDishGroupById(Guid id);
    Task<List<SideDishGroup>> GetByIdsAsync(List<Guid> ids);
    Task<List<ProductSideDishGroup>> GetAllSideDishesByProductId(Guid productId);
    Task<SideDish> GetSideDishById(Guid sideDishId);

}