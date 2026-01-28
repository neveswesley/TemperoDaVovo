using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;

public interface IGetAllProductUseCase
{
    Task<List<GetAllProductsResponse>> Execute(Guid restaurantId, string? search);
}