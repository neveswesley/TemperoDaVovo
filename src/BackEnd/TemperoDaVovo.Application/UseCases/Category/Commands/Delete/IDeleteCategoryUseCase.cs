namespace TemperoDaVovo.Application.UseCases.Category.Commands.Delete;

public interface IDeleteCategoryUseCase
{
    Task Execute (Guid categoryId);
}