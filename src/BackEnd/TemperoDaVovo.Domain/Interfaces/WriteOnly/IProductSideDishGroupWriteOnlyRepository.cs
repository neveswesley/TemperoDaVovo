using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IProductSideDishGroupWriteOnlyRepository
{
    Task<List<Guid>> GetLinkedGroupIdsAsync(Guid productId);
    Task AddAsync(ProductSideDishGroup link);
}