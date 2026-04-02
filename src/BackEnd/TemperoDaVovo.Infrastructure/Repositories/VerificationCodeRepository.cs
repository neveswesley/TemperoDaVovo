using Microsoft.EntityFrameworkCore;
using TemperoDaVovo.Domain.Entities;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Infrastructure.DataAccess;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class VerificationCodeRepository : IVerificationCodeReadOnlyRepository, IVerificationCodeWriteOnlyRepository
{
    private readonly AppDbContext _dbContext;

    public VerificationCodeRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<VerificationCode?> GetActiveCodeAsync(Guid userId, VerificationCodeType type)
    {
        return await _dbContext.VerificationCodes
            .Where(v => v.UserId == userId && v.Type == type && !v.IsUsed && v.IsActive)
            .OrderByDescending(v => v.CreatedAt)
            .FirstOrDefaultAsync();
    }

    public async Task AddAsync(VerificationCode code)
    {
        await _dbContext.VerificationCodes.AddAsync(code);
    }
}