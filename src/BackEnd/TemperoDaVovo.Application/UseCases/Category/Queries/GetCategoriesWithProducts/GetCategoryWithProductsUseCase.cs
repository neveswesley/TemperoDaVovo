using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;

namespace TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoriesWithProducts;

public class GetCategoryWithProductsUseCase : IGetCategoryWithProductsUseCase
{
    
    private readonly ICategoryReadOnlyRepository _repository;

    public GetCategoryWithProductsUseCase(ICategoryReadOnlyRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CategoryWithProductsResponseJson>> Execute(Guid restaurantId)
    {
        var categories = await _repository.GetCategoriesWithProducts(restaurantId);

        return categories.Select(c => new CategoryWithProductsResponseJson
        {
            CategoryId = c.Id,
            CategoryName = c.Name,
            Products = c.Products.Select(p => new ProductResponseJson
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Description = p.Description,
                ImageUrl = p.ImageUrl,
                IsActive = p.IsActive
            }).ToList()
        }).ToList();

    }
}