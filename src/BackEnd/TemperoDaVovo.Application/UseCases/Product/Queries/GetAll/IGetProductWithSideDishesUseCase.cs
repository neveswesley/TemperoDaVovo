using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;

public interface IGetProductWithSideDishesUseCase
{
    Task<List<GetProductWithSideDishesResponseJson>> Execute(Guid restaurantId, string? search);
}