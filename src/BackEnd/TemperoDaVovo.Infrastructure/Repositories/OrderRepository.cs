using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class OrderRepository : IOrderWriteOnlyRepository, IOrderReadOnlyRepository
{
    
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Guid> Create(Order order)
    {
        await _context.Orders.AddAsync(order);
        return order.Id;
    }

    public async Task<Guid> Update(Order order)
    {
        _context.Orders.Update(order);
        return await Task.FromResult(order.Id);
    }

    public async Task<Order?> GetOpenBySession(Guid restaurantId, string sessionId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .Where(o =>
                o.RestaurantId == restaurantId && o.ClientSessionId == sessionId &&
                o.Status == OrderStatus.PendingConfirmation)
            .OrderByDescending(o => o.CreatedAt)
            .FirstOrDefaultAsync();
    }
}