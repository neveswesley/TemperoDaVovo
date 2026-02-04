using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class ProductSideDishGroupRepository : IProductSideDishGroupWriteOnlyRepository, IProductSideDishGroupReadOnlyRepository
{
    
    private readonly AppDbContext _context;

    public ProductSideDishGroupRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Guid>> GetLinkedGroupIdsAsync(Guid productId)
    {
        return await _context.ProductSideDishGroups
            .Where(x => x.ProductId == productId)
            .Select(x => x.SideDishGroupId)
            .ToListAsync();
    }

    public async Task AddAsync(ProductSideDishGroup link)
    {
        await _context.ProductSideDishGroups.AddAsync(link);
    }


    public async Task <List<ProductSideDishGroup>> GetAllProductSideDishGroupsAsync(Guid productId)
    {
        return await _context.ProductSideDishGroups.Include(psg=>psg.SideDishGroup).ThenInclude(sdg=>sdg.SideDish).Where(psg=>psg.ProductId == productId).ToListAsync();
    }
}
