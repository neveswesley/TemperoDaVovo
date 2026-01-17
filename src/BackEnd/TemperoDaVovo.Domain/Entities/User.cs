namespace TemperoDaVovo.Domain.Entities;

public class User : BaseEntity
{
    public Guid RestaurantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Restaurant;
}