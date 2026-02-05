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
using TemperoDaVovo.Application.UseCases.SideDish.Commands.CreateSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.LinkGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.RemoveSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllProductSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllSideDishGroups;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetAllSideDishGroupsByProduct;
using TemperoDaVovo.Application.UseCases.SideDish.Queries.GetSideDishGroupsByProduct;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Commands;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Commands.CreateSideDish;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Commands.CreateSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDishGroup.Queries.GetAllSideDishGroups;
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
        services.AddScoped<ICreateSideDishGroupUseCase , CreateSideDishGroupUseCase >();
        services.AddScoped<ICreateSideDishUseCase , CreateSideDishUseCase >();
        services.AddScoped<IGetAllSideDishGroupsUseCase , GetAllSideDishGroupsUseCase >();
        services.AddScoped<ILinkSideDishSideDishGroupsToProductsToProductUseCase , LinkSideDishSideDishSideDishSideDishGroupsToProductsToProductsToProductsToProductUseCase >();
        services.AddScoped<IGetAllSideDishGroupByRestaurant0UseCase , GetAllSideDishGroupDishGroupByRestaurantUseCase >();
        services.AddScoped<IGetAllSideDishGroupsByProduct , GetAllSideDishGroupsByProduct >();
        services.AddScoped<IUpdateSideDishGroupUseCase , UpdateSideDishGroupUseCase >();
        services.AddScoped<IDeleteSideDishUseCase , DeleteSideDishUseCase >();
        services.AddScoped<IRemoveSideDishGroupUseCase , RemoveSideDishGroupUseCase >();
        services.AddScoped<IDeleteSideDishUseCase , DeleteSideDishUseCase >();
    }
}