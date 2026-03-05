namespace TemperoDaVovo.Application.Interfaces;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }
    Guid? RestaurantId { get; }
}