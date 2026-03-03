using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class PaymentRepository : IPaymentWriteOnlyRepository
{
    
    private readonly AppDbContext _dbContext;

    public PaymentRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Guid> CreateAsync(Payment payment)
    {
        await _dbContext.Payments.AddAsync(payment);
        return payment.Id;
    }
}