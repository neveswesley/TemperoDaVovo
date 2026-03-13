using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Communications.Responses;
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
            .Include(c => c.Neighborhoods.Where(n => n.IsActive))
            .Where(c => c.RestaurantId == restaurantId && c.IsActive)
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

    public void DeleteAsync(City city)
    {
        _dbContext.Cities.Remove(city);
    }

    public void UpdateAsync(City city)
    {
        _dbContext.Cities.Update(city);
    }
}