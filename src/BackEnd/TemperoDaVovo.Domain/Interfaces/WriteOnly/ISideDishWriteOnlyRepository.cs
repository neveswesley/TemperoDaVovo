using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ISideDishWriteOnlyRepository
{
    Task<SideDishGroup> CreateSideDishGroup(SideDishGroup sideDish);
    Task<SideDish> CreateSideDish(SideDish sideDish);
    Task<Guid> UpdateSideDishGroup(SideDishGroup sideDish);
    Task DeleteSideDishGroup(SideDishGroup sideDish);
    Task RemoveSideDishGroupsAsync(Guid productId, List<Guid> sideDishGroupIds);
    Task DeleteSideDish(Guid sideDishId);
    Task<Guid> ToggleActive(SideDish sideDish);
    Task<bool> AddComplementGroupsToProductAsync(Guid productId, List<Guid> complementGroupIds);

}