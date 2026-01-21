using TemperoDaVovo.Domain.Interfaces;

namespace TemperoDaVovo.Infrastructure.Repositories;

public class PasswordHasher : IPasswordHasher
{
    public string Hash(string password)
    {
       return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string hash)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}