namespace TemperoDaVovo.Application.Interfaces;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid? UserId { get; }
    Guid? RestaurantId { get; }
    string? Role { get; }

    void EnsureAuthenticated();
}