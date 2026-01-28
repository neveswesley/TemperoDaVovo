using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetById;

public interface IGetProductByIdUseCase
{
    Task<ProductResponseJson> Execute(Guid productId);
}