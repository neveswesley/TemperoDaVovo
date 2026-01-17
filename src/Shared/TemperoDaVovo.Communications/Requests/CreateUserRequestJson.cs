using TemperoDaVovo.Domain.Entities;

namespace TemperoDaVovo.Communications.Requests;

public class CreateUserRequestJson
{
    public Guid RestaurantId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.Restaurant;
}