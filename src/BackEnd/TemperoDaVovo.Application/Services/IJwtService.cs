namespace TemperoDaVovo.Application.Services;

public interface IJwtService
{
    string Generate(Guid userId, Guid restaurantId);
}