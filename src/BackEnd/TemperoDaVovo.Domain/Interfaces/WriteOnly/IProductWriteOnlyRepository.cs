using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IProductWriteOnlyRepository
{
    Task<Guid> CreateProduct(Product product);
    Task<Guid> UpdateProduct(Product product);
    Task DeleteProduct(Guid productId);
    Task<Guid> DeactivateProduct(Product product);
    Task<Guid> ActiveProduct(Product product);
    Task<Guid> ToggleActive(Product product);
    Task<Guid> UpdateProduct(Guid id);
}