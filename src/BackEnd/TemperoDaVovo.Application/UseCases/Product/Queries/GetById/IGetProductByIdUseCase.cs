using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetById;

public interface IGetProductByIdUseCase
{
    Task<GetProductWithSideDishesResponseJson> ExecuteAsync(Guid productId);
}