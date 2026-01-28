using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetById;

public class GetProductByIdUseCase : IGetProductByIdUseCase
{
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetProductByIdUseCase(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<ProductResponseJson> Execute(Guid productId)
    {
        var product = await _productReadOnlyRepository.GetProductByIdWithCategory(productId);

        if (product == null)
            throw new KeyNotFoundException("Produto não encontrado.");

        return new ProductResponseJson
        {
            Id = product.Id,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            IsActive = product.IsActive,
            Category = product.Category != null ? new CategoryResponseJson()
            {
                Id = product.Category.Id,
                CategoryId = product.Category.Id,
                CategoryName = product.Category.Name
            } : null
        };
    }
}