using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class SideDishRepository : ISideDishReadOnlyRepository, ISideDishWriteOnlyRepository
{

    private readonly AppDbContext _context;

    public SideDishRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<SideDishGroup> CreateSideDishGroup(SideDishGroup sideDish)
    {
        await _context.SideDishesGroups.AddAsync(sideDish);
        return sideDish;
    }

    public async Task<SideDish> CreateSideDish(SideDish sideDish)
    {
        await _context.SideDishes.AddAsync(sideDish);
        return sideDish;
    }

    public async Task<Guid> UpdateSideDishGroup(SideDishGroup sideDishGroup)
    {
        _context.SideDishesGroups.Update(sideDishGroup);
        return await Task.FromResult(sideDishGroup.Id);
    }

    public Task DeleteSideDishGroup(SideDishGroup sideDish)
    {
        _context.SideDishesGroups.Remove(sideDish);
        return Task.CompletedTask;
    }

    public async Task RemoveSideDishGroupsAsync(Guid productId, List<Guid> sideDishGroupIds)
    {
        await _context.ProductSideDishGroups
            .Where(x => x.ProductId == productId && sideDishGroupIds.Contains(x.SideDishGroupId)).ExecuteDeleteAsync();
    }

    public async Task DeleteSideDish(Guid sideDishId)
    {
        await _context.SideDishes.Where(s=>s.Id == sideDishId).ExecuteDeleteAsync();
    }

    public async Task<List<string>> GetExistingSideDishNames(Guid restaurantId, string name)
    {
        return await _context.SideDishesGroups.Where(s=>s.RestaurantId == restaurantId && (s.Name == name || s.Name.StartsWith(name + " ("))).Select(s => s.Name).ToListAsync();
    }

    public async Task<List<SideDishGroup>> GetAllSideDishesByRestaurant(Guid restaurantId)
    {
        return await _context.SideDishesGroups.Where(x=>x.RestaurantId == restaurantId).ToListAsync();
    }

    public async Task<SideDishGroup> GetSideDishGroupById(Guid id)
    {
        return await _context.SideDishesGroups.FirstOrDefaultAsync(s=>s.Id == id);
    }

    public async Task<List<SideDishGroup>> GetByIdsAsync(List<Guid> ids)
    {
        return await _context.SideDishesGroups.Where(x => ids.Contains(x.Id)).ToListAsync();
    }

    public async Task<List<ProductSideDishGroup>> GetAllSideDishesByProductId(Guid productId)
    {
        return await _context.ProductSideDishGroups.Where(s=>s.ProductId == productId).ToListAsync();
    }

    public async Task<SideDish> GetSideDishById(Guid sideDishId)
    {
        return await _context.SideDishes.FirstOrDefaultAsync(s=>s.Id == sideDishId);
    }
}