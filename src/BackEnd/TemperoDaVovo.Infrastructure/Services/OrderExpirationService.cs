using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TemperoDaVovo.Domain.Enums;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Services;

public class OrderExpirationService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public OrderExpirationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            var expirationTime = DateTime.UtcNow.AddMinutes(-8);

            var expiredOrders = await context.Orders
                .Where(o => o.Status == OrderStatus.PendingConfirmation &&
                            o.CreatedAt <= expirationTime)
                .ToListAsync();

            foreach (var order in expiredOrders)
            {
                order.Cancel(CancellationReasonType.NotConfirmedByRestaurant, CanceledBy.System, "O restaurante não confirmou o pedido a tempo.");
            }

            await context.SaveChangesAsync(stoppingToken);

            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}