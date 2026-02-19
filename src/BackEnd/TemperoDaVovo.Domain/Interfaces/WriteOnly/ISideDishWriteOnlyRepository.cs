using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ISideDishWriteOnlyRepository
{
    Task<SideDishGroup> CreateGroupAsync(SideDishGroup sideDish);
    Task<SideDish> CreateAsync(SideDish sideDish);
    Task<Guid> UpdateGroupAsync(SideDishGroup sideDish);
    Task DeleteGroupAsync(Guid groupId);
    Task RemoveGroupsAsync(Guid productId, List<Guid> sideDishGroupIds);
    Task DeleteAsync(Guid sideDishId);
    Task<Guid> ToggleActive(SideDish sideDish);
    Task<bool> AddComplementGroupsToProductAsync(Guid productId, List<Guid> complementGroupIds);
}