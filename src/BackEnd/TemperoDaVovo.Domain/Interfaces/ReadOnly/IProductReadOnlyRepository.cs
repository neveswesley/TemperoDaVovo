using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IProductReadOnlyRepository
{
    Task<List<Product>> GetAllProductByRestaurantWithSideDish(Guid restaurantId, string? search);
    Task<Product> GetProductByRestaurantId(Guid restaurantId, Guid productId);
    Task<Product> GetProductByIdWithCategory (Guid productId);
}