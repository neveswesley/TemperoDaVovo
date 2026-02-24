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
        var tracked = await _context.Orders
            .Include(o => EF.Property<ICollection<OrderItem>>(o, "_items"))
            .ThenInclude(i => i.SideDishes)
            .FirstAsync(o => o.Id == order.Id);

        tracked.CalculateTotals();
        return tracked.Id;
    }

    public async Task AddItemToExistingOrder(OrderItem item)
    {
        await _context.OrderItems.AddAsync(item);
    }

    public async Task UpdateOrderItem(OrderItem orderItem, CancellationToken ct = default)
    {
        var oldSideDishes = await _context.Set<OrderItemSideDish>()
            .Where(x => x.OrderItemId == orderItem.Id)
            .ToListAsync(ct);

        _context.Set<OrderItemSideDish>().RemoveRange(oldSideDishes);

        await _context.Set<OrderItemSideDish>().AddRangeAsync(orderItem.SideDishes, ct);

        var tracked = await _context.OrderItems.FirstAsync(x => x.Id == orderItem.Id, ct);
        _context.Entry(tracked).CurrentValues.SetValues(orderItem);
    }

    public async Task<Order?> GetOpenBySession(Guid restaurantId, string sessionId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .FirstOrDefaultAsync(o =>
                o.RestaurantId == restaurantId &&
                o.ClientSessionId == sessionId &&
                o.Status == OrderStatus.PendingConfirmation);
    }

    public async Task<OrderItem?> GetOrderItemById(Guid orderItemId)
    {
        return await _context.OrderItems
            .Include(i => i.SideDishes)
            .AsNoTracking()
            .FirstOrDefaultAsync(o => o.Id == orderItemId);
    }

    public async Task<OrderItem?> GetTrackedById(Guid id)
    {
        return await _context.OrderItems
            .Include(i => i.SideDishes)
            .FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<OrderItem?> GetByIdWithSideDishesAsync(Guid orderItemId, CancellationToken ct = default)
    {
        return await _context.OrderItems
            .Include(oi => oi.SideDishes)
            .FirstOrDefaultAsync(oi => oi.Id == orderItemId, ct);
    }

    public async Task<Order?> GetOrderById(Guid orderId)
    {
        return await _context.Orders
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task RemoveItemByCart(Guid orderItemId)
    {
        var item = await _context.OrderItems
            .Include(i => i.SideDishes)
            .FirstOrDefaultAsync(o => o.Id == orderItemId);
    
        if (item != null)
            _context.OrderItems.Remove(item);
    }
}