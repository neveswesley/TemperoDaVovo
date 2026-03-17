using TemperoDaVovo.Domain.Interfaces;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Domain.Entities;

public class User : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Restaurant;
    
    public void UpdatePassword(string currentPassword, string newPassword, IPasswordHasher passwordHasher)
    {
        var passwordIsValid = passwordHasher.Verify(currentPassword, PasswordHash);

        if (!passwordIsValid)
            throw new ErrorOnValidationException(["Senha atual inválida"]);

        PasswordHash = passwordHasher.Hash(newPassword);
    }
    
}