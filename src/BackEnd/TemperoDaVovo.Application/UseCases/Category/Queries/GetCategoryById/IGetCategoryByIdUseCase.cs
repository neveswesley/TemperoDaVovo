namespace TemperoDaVovo.Application.UseCases.Category.Queries.GetCategoryById;

public interface IGetCategoryByIdUseCase
{
    Task<Domain.Entities.Category> GetCategoryById(Guid categoryId);
}