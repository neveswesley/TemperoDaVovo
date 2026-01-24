using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IProductReadOnlyRepository
{
    Task<List<Product>> GetAllProductByRestaurantId(Guid restaurantId);
    Task<Product> GetProductByIdWithCategory (Guid productId);
}