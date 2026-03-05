using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class NeighborhoodRepository : INeighborhoodWriteOnlyRepository, INeighborhoodReadOnlyRepository
{
    private readonly AppDbContext _dbContext;

    public NeighborhoodRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Neighborhood> AddAsync(Neighborhood neighborhood)
    {
        await _dbContext.Neighborhoods.AddAsync(neighborhood);
        return neighborhood;
    }

    public async Task<bool> ExistingNameByCity(string name, Guid  cityId)
    {
        var cityName = name.ToLower();
        return await _dbContext.Neighborhoods.Where(n=>n.CityId == cityId).AnyAsync(n=>n.Name.ToLower() == cityName);
    }

    public async Task<List<Neighborhood>> GetNeighborhoodByRestaurantId(Guid restaurantId)
    {
        return await _dbContext.Neighborhoods.Include(n=>n.City).Where(n=>n.City.RestaurantId == restaurantId).ToListAsync();
        
    }

    public async Task<Neighborhood> GetNeighborhoodById(Guid? neighborhoodId)
    {
        return await _dbContext.Neighborhoods.FirstOrDefaultAsync(n => n.Id == neighborhoodId);
    }
}