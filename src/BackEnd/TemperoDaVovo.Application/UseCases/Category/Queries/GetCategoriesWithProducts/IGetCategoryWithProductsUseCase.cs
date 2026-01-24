using TemperoDaVovo.Communications.Responses;

namespace TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoriesWithProducts;

public interface IGetCategoryWithProductsUseCase
{
    Task<List<CategoryWithProductsResponseJson>> Execute(Guid restaurantId);
}