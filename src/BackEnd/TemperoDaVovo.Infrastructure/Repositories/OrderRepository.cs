using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Common;
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
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .FirstAsync(o => o.Id == order.Id);

        _context.Entry(tracked).CurrentValues.SetValues(order);

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
        return await _context.Orders.
            Include(o=>o.Neighborhood)
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .FirstOrDefaultAsync(o =>
                o.RestaurantId == restaurantId &&
                o.ClientSessionId == sessionId &&
                o.Status == OrderStatus.Draft);
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

    public async Task<string?> ExistingPhone(string name)
    {
        var orders = await _context.Orders.ToListAsync();
        foreach (var order in orders)
        {
            if (order.CustomerPhone == name)
                return order.CustomerName;
        }

        return null;
    }

    public async Task<List<Order>> GetOrdersByClientId(string sessionId)
    {
        return await _context.Orders
            .Include(o=>o.Neighborhood)
            .Include(o => o.Payment)
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .Where(o => o.ClientSessionId == sessionId)
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }
    
    public async Task<PaginatedResponse<Order>> GetOrderHistoryByRestaurantId(
        Guid restaurantId,
        int page,
        int pageSize)
    {
        var query = _context.Orders
            .AsNoTracking()
            .AsSplitQuery()
            .Include(o => o.Neighborhood)
            .Include(o => o.Payment)
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .Where(o => o.RestaurantId == restaurantId
                        && o.Status != OrderStatus.Draft
                        && o.Status != OrderStatus.Abandoned)
            .OrderByDescending(o => o.CreatedAt);

        var totalItems = await query.CountAsync();

        var orders = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedResponse<Order>
        {
            Page = page,
            PageSize = pageSize,
            TotalItems = totalItems,
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize),
            Data = orders
        };
    }

    public async Task<List<Order>> GetActiveOrdersByRestaurantId(Guid restaurantId)
    {
        return await _context.Orders
            .AsNoTracking()
            .AsSplitQuery()
            .Include(o => o.Neighborhood)
            .Include(o => o.Payment)
            .Include(o => o.Items)
            .ThenInclude(i => i.SideDishes)
            .Where(o =>
                o.RestaurantId == restaurantId &&
                (o.Status == OrderStatus.PendingConfirmation ||
                 o.Status == OrderStatus.Preparing ||
                 o.Status == OrderStatus.Ready ||
                 o.Status == OrderStatus.OnTheWay))
            .OrderByDescending(o => o.CreatedAt)
            .ToListAsync();
    }

    public Task<bool> ExistingClient(string sessionId)
    {
        return _context.Orders.AnyAsync(o => o.ClientSessionId == sessionId);
    }

    public async Task RemoveItemByCart(Guid orderItemId)
    {
        var item = await _context.OrderItems
            .Include(i => i.SideDishes)
            .FirstOrDefaultAsync(o => o.Id == orderItemId);
    
        if (item != null)
            _context.OrderItems.Remove(item);
    }

    public Task<Guid> FinalizeOrder(Order order)
    {
        _context.Orders.Update(order);
        return Task.FromResult(order.Id);
    }
    
    public async Task<int> GetNextOrderNumber()
    {
        var random = new Random();
        int number;
        bool exists;

        do
        {
            number = random.Next(1000, 10000);
            exists = await _context.Orders
                .AnyAsync(o => o.OrderNumber == number);

        } while (exists);

        return number;
    }
}