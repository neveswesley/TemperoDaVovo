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


    public async Task<List<Product>> GetAllProductByRestaurantId(Guid restaurantId)
    {
        return await _context.Products.Where(p=>p.RestaurantId==restaurantId).ToListAsync();
        
    }
}