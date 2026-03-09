using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class CityRepository : ICityReadOnlyRepository, ICityWriteOnlyRepository
{
    
    private readonly AppDbContext _dbContext;

    public CityRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<City> GetByIdAsync(Guid cityId)
    {
        return await _dbContext.Cities.FirstOrDefaultAsync(c => c.Id == cityId);
    }

    public async Task<List<City>> GetAll(Guid restaurantId)
    {
        return await _dbContext.Cities
            .Include(c => c.Neighborhoods)
            .Where(c => c.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<bool> CityExistingByRestaurantId(string cityName, Guid restaurantId)
    {
        var city = cityName.ToLower();
        return await _dbContext.Cities.Where(c=>c.RestaurantId == restaurantId).AnyAsync(c => c.Name.ToLower() == city);
    }

    public async Task<Guid> CreateAsync(City city)
    {
        await _dbContext.Cities.AddAsync(city);
        return city.Id;
    }

    public Task DeleteAsync(City city)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> UpdateAsync(City city)
    {
        _dbContext.Cities.Update(city);
        return await Task.FromResult(city.Id);
    }
}