using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ISideDishWriteOnlyRepository
{
    Task<SideDishGroup> CreateSideDishGroup(SideDishGroup sideDish);
    Task<SideDish> CreateSideDish(SideDish sideDish);
    Task<Guid> UpdateSideDishGroup(SideDishGroup sideDish);
    Task DeleteSideDishGroup(SideDishGroup sideDish);
    Task RemoveSidhDishGroupsAsync(Guid productId, List<Guid> sideDishGroupIds);
}