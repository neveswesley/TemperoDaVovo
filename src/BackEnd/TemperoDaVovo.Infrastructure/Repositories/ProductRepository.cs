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

    public async Task<Guid> CreateProduct(Product product)
    {
        await _context.Products.AddAsync(product);
        return product.Id;
    }

    public async Task<Guid> UpdateProduct(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return product.Id;
    }

    public async Task DeleteProduct(Guid productId)
    {
        var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
        _context.Products.Remove(product);
    }

    public async Task<Guid> DeactivateProduct(Product product)
    {
         _context.Products.Update(product);
         return await Task.FromResult(product.Id);
    }

    public async Task<Guid> ActiveProduct(Product product)
    {
        _context.Update(product);
        return await Task.FromResult(product.Id);
    }

    public async Task<Guid> ToggleActive(Product product)
    {
        var entity = await _context.Products.FirstOrDefaultAsync(x => x.Id == product.Id);
        if (entity == null)
        {
            throw new KeyNotFoundException($"Produto com ID {product.Id} não encontrado.");
        }
        
        entity.IsActive = product.IsActive;
        
        _context.Products.Update(entity);

        return entity.Id;
    }


    public async Task<List<Product>> GetAllProductByRestaurantId(Guid restaurantId)
    {
        return await _context.Products.Include(p=> p.Category).Where(p=>p.RestaurantId==restaurantId && p.IsActive == true).ToListAsync();
    }

    public async Task<Product> GetProductByIdWithCategory(Guid productId)
    {
        return await _context.Products.Include(c=>c.Category).FirstOrDefaultAsync(p => p.Id == productId);
    }
}