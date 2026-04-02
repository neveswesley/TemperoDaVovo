using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;

public interface IGetProductWithSideDishesUseCase
{
    Task<List<GetProductWithSideDishesResponseJson>> ExecuteAsync(Guid restaurantId, string? search);
}