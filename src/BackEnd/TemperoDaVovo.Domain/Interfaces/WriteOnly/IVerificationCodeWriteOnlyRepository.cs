using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IVerificationCodeWriteOnlyRepository
{
    Task AddAsync(VerificationCode code);
}