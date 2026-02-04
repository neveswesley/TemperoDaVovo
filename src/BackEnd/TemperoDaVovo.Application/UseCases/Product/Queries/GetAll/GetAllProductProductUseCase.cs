using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Product.Queries.GetAll;

public class GetAllProductProductUseCase : IGetAllProductUseCase
{
    
    private readonly IProductReadOnlyRepository _productReadOnlyRepository;

    public GetAllProductProductUseCase(IProductReadOnlyRepository productReadOnlyRepository)
    {
        _productReadOnlyRepository = productReadOnlyRepository;
    }

    public async Task<List<GetAllProductsResponseJson>> Execute(Guid restaurantId, string? search)
    {
        var products = await _productReadOnlyRepository.GetAllProductByRestaurantId(restaurantId, search);

        var response = products.Select(p => new GetAllProductsResponseJson
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Category = p.Category?.Name,
                IsActive = p.IsActive,
                Description = p.Description,
            })
            .ToList();

        return response;
    }
}