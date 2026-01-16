using Microsoft.Extensions.DependencyInjection;
using TemperoDaVovo.Application.UseCases.Restaurant;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;

namespace TemperoDaVovo.Application;

public static class ServicesExtensions
{
    public static IServiceCollection ConfigureApplicationApp(this IServiceCollection services)
    {
        AddUseCases(services);
        return services;
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<ICreateRestaurantUseCase, CreateRestaurantUseCase>();
    }
}