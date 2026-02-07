using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class CategoryRepository : ICategoryWriteOnlyRepository, ICategoryReadOnlyRepository
{
    
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Category> CreateAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        return category;
    }

    public Task<Guid> UpdateAsync(Category category)
    {
        _context.Update(category);
        return Task.FromResult(category.Id);
    }

    public async Task DeleteAsync(Guid categoryId)
    {
        var category  = await _context.Categories.FirstOrDefaultAsync(x=>x.Id == categoryId);
        _context.Remove(category);
    }

    public async Task<bool> UpdateCategoryOrderAsync(Guid restaurantId, List<Guid> categoryIds)
    {
        var categories = await _context.Categories
            .Where(c => c.RestaurantId == restaurantId)
            .ToListAsync();
        
        for (int i = 0; i < categoryIds.Count; i++)
        {
            var category = categories.FirstOrDefault(c => c.Id == categoryIds[i]);
            if (category != null)
            {
                category.DisplayOrder = i;
            }
        }

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<List<string>> GetExistingCategoryNames(Guid restaurantId, string name)
    {
        return await _context.Categories
            .Where(c =>
                c.RestaurantId == restaurantId &&
                (c.Name == name || c.Name.StartsWith(name + " ("))
            )
            .Select(c => c.Name)
            .ToListAsync();
    }

    public async Task<List<Category>> GetCategoriesWithProducts(Guid restaurantId)
    {
        return await _context.Categories
            .Include(c => c.Products)
            .OrderBy(c=>c.DisplayOrder)
            .Where(c => c.RestaurantId == restaurantId && c.IsActive == true)
            .ToListAsync();
    }

    public async Task<Category> GetCategoryById(Guid categoryId)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
    }

    public async Task<List<Category>> GetCategoriesByRestaurantId(Guid restaurantId)
    {
        return await _context.Categories
            .Where(c => c.RestaurantId == restaurantId)
            .OrderBy(c => c.DisplayOrder)
            .ThenBy(c => c.CreatedAt)
            .Include(c => c.Products)
            .ToListAsync();
    }
}