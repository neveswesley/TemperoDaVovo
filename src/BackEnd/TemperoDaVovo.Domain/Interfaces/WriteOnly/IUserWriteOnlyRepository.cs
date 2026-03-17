using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Domain.Interfaces.WriteOnly;

public interface IUserWriteOnlyRepository
{
    Task<Guid> RegisterUser(User user);
    void Update(User user);
}