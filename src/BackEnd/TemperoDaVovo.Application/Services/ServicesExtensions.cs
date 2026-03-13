using Microsoft.Extensions.DependencyInjection;
using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.UseCases.Category.Commands;
using TemperoDaVovo.Application.UseCases.Category.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Category.Commands.Reorder;
using TemperoDaVovo.Application.UseCases.Category.Commands.UpdateProduct;
using TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoriesWithProducts;
using TemperoDaVovo.Application.UseCases.City.Commands.Create;
using TemperoDaVovo.Application.UseCases.City.Commands.Delete;
using TemperoDaVovo.Application.UseCases.City.Commands.Update;
using TemperoDaVovo.Application.UseCases.City.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.City.Queries.GetById;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Update;
using TemperoDaVovo.Application.UseCases.Neighborhood.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.Order.Commands.AcceptOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.AddItemToOrder;
using TemperoDaVovo.Application.UseCases.Order.Commands.Cancel;
using TemperoDaVovo.Application.UseCases.Order.Commands.ChangeOrderStatus;
using TemperoDaVovo.Application.UseCases.Order.Commands.CompleteCheckout;
using TemperoDaVovo.Application.UseCases.Order.Commands.ExistingPhone;
using TemperoDaVovo.Application.UseCases.Order.Commands.Finalize;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveAll;
using TemperoDaVovo.Application.UseCases.Order.Commands.RemoveOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Commands.UpdateOrderItem;
using TemperoDaVovo.Application.UseCases.Order.Queries.CurrentOrder;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByClient;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByCliente;
using TemperoDaVovo.Application.UseCases.Order.Queries.GetOrderByRestaurant;
using TemperoDaVovo.Application.UseCases.Product.Commands.Create;
using TemperoDaVovo.Application.UseCases.Product.Commands.Delete;
using TemperoDaVovo.Application.UseCases.Product.Commands.Duplicate;
using TemperoDaVovo.Application.UseCases.Product.Commands.ToggleProductActive;
using TemperoDaVovo.Application.UseCases.Product.Commands.Update;
using TemperoDaVovo.Application.UseCases.Product.Commands.UpdateImage;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;
using TemperoDaVovo.Application.UseCases.Product.Queries.GetById;
using TemperoDaVovo.Application.UseCases.Restaurant.Create;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.CreateSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDish;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.LinkGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.RemoveSideDishGroup;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.ToggleSideDishActive;
using TemperoDaVovo.Application.UseCases.SideDish.Commands.UpdateSideDish;
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
        services.AddScoped<IGetProductWithSideDishesUseCase, GetProductWithSideDishesProductWithSideDishesUseCase>();
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
        services.AddScoped<IDeleteSideDishGroupUseCase , DeleteSideDishGroupUseCase >();
        services.AddScoped<IRemoveSideDishGroupUseCase , RemoveSideDishGroupUseCase >();
        services.AddScoped<IDeleteSideDishGroupUseCase , DeleteSideDishGroupUseCase >();
        services.AddScoped<IUpdateSideDishUseCase , UpdateSideDishUseCase >();
        services.AddScoped<IToggleSideDishActiveUseCase , ToggleSideDishActiveUseCase >();
        services.AddScoped<IReorderCategoriesUseCase , ReorderCategoriesUseCase >();
        services.AddScoped<IDuplicateProductUseCase , DuplicateProductUseCase >();
        services.AddScoped<IDeleteSideDishUseCase , DeleteSideDishUseCase >();
        services.AddScoped<IAddItemToOrderUseCase , AddItemToOrderUseCase >();
        services.AddScoped<IGetCurrentOrderUseCase , GetCurrentOrderUseCase >();
        services.AddScoped<IUpdateOrderItemUseCase , UpdateOrderItemUseCase >();
        services.AddScoped<IRemoveOrderItemUseCase , RemoveOrderItemUseCase >();
        services.AddScoped<IRemoveAllOrderItemUseCase , RemoveAllOrderItemUseCase >();
        services.AddScoped<ICompleteCheckoutUseCase , CompleteCheckoutUseCase >();
        services.AddScoped<IExistingPhoneUseCase , ExistingPhoneUseCase >();
        services.AddScoped<ICreateNeighborhoodUseCase , CreateNeighborhoodUseCase >();
        services.AddScoped<IGetAllNeighborhoodByRestaurantId , GetAllNeighborhoodByRestaurantId >();
        services.AddScoped<ICreateCityUseCase , CreateCityUseCase >();
        services.AddScoped<IGetCityByIdUseCase , GetCityByIdUseCase >();
        services.AddScoped<IGetAllCitiesByRestaurantId , GetAllCitiesByRestaurantId >();
        services.AddScoped<IFinalizeOrderUseCase , FinalizeOrderUseCase >();
        services.AddScoped<IGetOrderByClientUseCase , GetOrderByClientUseCase >();
        services.AddScoped<ICancelOrderUseCase , CancelOrderUseCase >();
        services.AddScoped<IGetOrderByRestaurantId , GetOrderByRestaurantId >();
        services.AddScoped<IChangeOrderStatusUseCase , ChangeOrderStatusUseCase >();
        services.AddScoped<IUpdateCityUseCase , UpdateCityUseCase >();
        services.AddScoped<IDeleteCityUseCase , DeleteCityUseCase >();
        services.AddScoped<IUpdateNeighborhoodUseCase , UpdateNeighborhoodUseCase >();
        services.AddScoped<IDeleteNeighborhoodUseCase , DeleteNeighborhoodUseCase >();
    }
}