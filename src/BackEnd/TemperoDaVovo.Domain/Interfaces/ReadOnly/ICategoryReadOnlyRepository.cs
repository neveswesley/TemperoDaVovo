using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface ICategoryReadOnlyRepository
{
    Task<List<string>> GetExistingCategoryNames(Guid restaurantId, string name);
    Task<List<Category>> GetCategoriesWithProducts(Guid restaurantId);
    Task<Category> GetCategoryById(Guid categoryId);
}