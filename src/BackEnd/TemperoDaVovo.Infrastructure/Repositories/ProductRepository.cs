using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class ProductRepository : IProductWriteOnlyRepository, IProductReadOnlyRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Product> CreateProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        return product;
    }

    public async Task<Guid> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public void DeleteProduct(Guid productId)
    {
        var product = _context.Products.FirstOrDefault(p => p.Id == productId);
        product.IsActive = false;
        product.DeletedAt = DateTime.UtcNow;
    }

    public async Task<Guid> ToggleActive(Product product)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id && x.IsActive == true);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Produto com ID {product.Id} não encontrado.");
        }

        entity.IsPaused = product.IsPaused;

        _context.Products.Update(entity);

        return entity.Id;
    }

    public async Task<Guid> UpdateProduct(Guid id)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id && p.IsActive == true);
        _context.Products.Update(product);
        return await Task.FromResult(product.Id);
    }


    public async Task<List<Product>> GetAllProductByRestaurantWithSideDish(Guid restaurantId, string? search)
    {
        var query = _context.Products.Where(p => p.RestaurantId == restaurantId && p.IsActive == true);

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => p.Name.Contains(search) || p.Description.Contains(search));
        }

        return await query
            .Include(p => p.Category)
            .Include(p => p.ProductSideDishGroups)
            .ThenInclude(psdg => psdg.SideDishGroup)
            .ThenInclude(sdg => sdg.SideDish)
            .OrderBy(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<Product> GetProductByRestaurantId(Guid restaurantId, Guid productId)
    {
        return await _context.Products.FirstOrDefaultAsync(p =>
            p.Id == productId && p.RestaurantId == restaurantId && p.IsActive == true);
    }

    public async Task<Product> GetProductByIdWithCategory(Guid productId)
    {
        return await _context.Products.Include(c => c.Category).Include(c => c.ProductSideDishGroups)
            .ThenInclude(c => c.SideDishGroup).ThenInclude(c => c.SideDish)
            .FirstOrDefaultAsync(p => p.Id == productId && p.IsActive == true);
    }
}