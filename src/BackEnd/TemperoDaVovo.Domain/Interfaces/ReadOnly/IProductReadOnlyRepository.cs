using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IProductReadOnlyRepository
{
    Task<List<Product>> GetAllProductByRestaurantId(Guid restaurantId, string? search);
    Task<Product> GetProductByRestaurantId(Guid restaurantId, Guid productId);
    Task<Product> GetProductByIdWithCategory (Guid productId);
}