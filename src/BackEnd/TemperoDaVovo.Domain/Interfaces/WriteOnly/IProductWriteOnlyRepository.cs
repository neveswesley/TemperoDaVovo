using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IProductWriteOnlyRepository
{
    Task<Guid> CreateProduct(Product product);
}