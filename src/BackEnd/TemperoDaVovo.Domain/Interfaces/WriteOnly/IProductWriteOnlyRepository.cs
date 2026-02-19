using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IProductWriteOnlyRepository
{
    Task<Product> CreateProduct(Product product);
    Task<Guid> UpdateProduct(Product product);
    void DeleteProduct(Guid productId);
    Task<Guid> ToggleActive(Product product);
    Task<Guid> UpdateProduct(Guid id);
}