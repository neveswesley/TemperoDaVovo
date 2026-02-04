using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IProductSideDishGroupReadOnlyRepository
{
    Task <List<ProductSideDishGroup>> GetAllProductSideDishGroupsAsync(Guid productId);
}