using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.ReadOnly;

public interface IVerificationCodeReadOnlyRepository
{
    Task<VerificationCode?> GetActiveCodeAsync(Guid userId, VerificationCodeType type);
}