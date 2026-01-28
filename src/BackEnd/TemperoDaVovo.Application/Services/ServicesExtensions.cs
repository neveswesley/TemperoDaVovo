using Microsoft.Extensions.DependencyInjection;
using TemperoDaVovo.Application.UseCases.Category.Commands;
using TemperoDaVovo.Application.UseCases.Category.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Category.Commands.UpdateProduct;
using TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoriesWithProducts;
using TemperoDaVovo.Application.UseCases.Product.Commands.Create;
using TemperoDaVovo.Application.UseCases.Product.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;
using TemperoDaVovo.Application.UseCases.Product.Commands.Update;
using TemperoDaVovo.Application.UseCases.Product.Commands.UpdateImage;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetById;
using TemperoDaVovo.Application.UseCases.Restaurant;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Application.UseCases.User.Create;
using TemperoDaVovo.Application.UseCases.User.Login;

namespace TemperoDaVovo.Application.Services;

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
        services.AddScoped<ICreateCategoryUseCase, CreateCategoryUseCase>();
        services.AddScoped<IUpdateCategoryUseCase, UpdateCategoryUseCase>();
        services.AddScoped<IGetCategoryWithProductsUseCase, GetCategoryWithProductsUseCase>();
        services.AddScoped<IDeleteProductUseCase, DeleteProductUseCase>();
        services.AddScoped<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
        services.AddScoped<IToggleProductActiveUseCase, ToggleProductActiveUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IUpdateProductImageUseCase , UpdateProductImageUseCase >();
    }
}