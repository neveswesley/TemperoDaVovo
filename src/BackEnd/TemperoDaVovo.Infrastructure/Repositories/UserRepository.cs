using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class UserRepository : IUserWriteOnlyRepository, IUserReadOnlyRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> RegisterUser(User user)
    {
        await _context.Users.AddAsync(user);
        return user.Id;
    }

    public void Update(User user)
    {
        _context.Users.Update(user);
    }

    public async Task<bool> EmailExists(string email)
    {
       return await _context.Users.AnyAsync(x=>x.Email.Equals(email));
    }

    
    public async Task<bool> RestaurantHasAnyUser(Guid restaurantId)
    {
        return await _context.Users.AnyAsync(x => x.RestaurantId == restaurantId);
    }

    public async Task<User> GetByEmail(string email)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }
}