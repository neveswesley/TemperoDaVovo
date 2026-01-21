using Microsoft.Extensions.DependencyInjection;
using TemperoDaVovo.Application.UseCases.Product.Commands.Create;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.Restaurant;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Application.UseCases.User.Login;

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
        services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
        services.AddScoped<ICreateProductUseCase, CreateProductUseCase>();
        services.AddScoped<ILoginUseCase, LoginUseCase>();
        services.AddScoped<IGetAllProductUseCase, GetAllProductProductUseCase>();
    }
}