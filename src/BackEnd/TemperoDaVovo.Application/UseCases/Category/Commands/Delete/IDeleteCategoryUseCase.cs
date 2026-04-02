namespace TemperoDaVovo.Application.UseCases.Category.Commands.Delete;

public interface IDeleteCategoryUseCase
{
    Task ExecuteAsync (Guid categoryId);
}