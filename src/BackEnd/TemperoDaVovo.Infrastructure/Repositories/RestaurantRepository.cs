using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantReadOnlyRepository, IRestaurantWriteOnlyRepository
{
    
    private readonly AppDbContext _context;

    public RestaurantRepository(AppDbContext context)
    {
        _context = context;
    }


    public async Task<Guid> AddAsync(Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        return restaurant.Id;
    }

    public void Update(Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
    }

    public void RemoveOpeningHours(List<RestaurantOpeningHour> hours)
    {
        _context.Set<RestaurantOpeningHour>().RemoveRange(hours);
    }

    public async Task<bool> ExistActiveRestaurantWithPhone(string phone)
    {
        var restaurant = await _context.Restaurants.AnyAsync(r=>r.Phone.Equals(phone));
        return restaurant;
    }

    public Task<bool> PhoneExists(string phone)
    {
        return _context.Restaurants.AnyAsync(r => r.Phone.Equals(phone));
    }

    public Task<bool> RestaurantExists(Guid restaurantId)
    {
        return _context.Restaurants.AnyAsync(r => r.Id == restaurantId);
    }

    public async Task<Restaurant?> GetRestaurantById(Guid restaurantId)
    {
        return await _context.Restaurants.FirstOrDefaultAsync(r => r.Id == restaurantId);
    }

    public async Task<Restaurant?> GetByIdWithOpeningHours(Guid restaurantId)
    {
        return await _context.Restaurants
            .Where(r => r.Id == restaurantId)
            .Include(r => r.OpeningHours)
            .Include(r=>r.Neighborhoods)
            .FirstOrDefaultAsync();
    }

    public void DetectChange(Restaurant restaurant)
    {
        _context.Restaurants.Entry(restaurant).State = EntityState.Modified;

        foreach (var openingHour in restaurant.OpeningHours)
        {
            _context.RestaurantOpeningHours.Entry(openingHour).State = EntityState.Added;
        }
    }
}