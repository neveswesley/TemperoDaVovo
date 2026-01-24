using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface ICategoryWriteOnlyRepository
{
    Task<Category> CreateAsync(Category category);
    Task<Guid> UpdateAsync(Category category);
    Task DeleteAsync(Guid categoryId);
}