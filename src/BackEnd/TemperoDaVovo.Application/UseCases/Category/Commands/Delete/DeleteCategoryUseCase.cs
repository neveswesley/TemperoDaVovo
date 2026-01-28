using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _write;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryUseCase(ICategoryWriteOnlyRepository write, IUnitOfWork unitOfWork)
    {
        _write = write;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(Guid categoryId)
    {
        await _write.DeleteAsync(categoryId);
        await _unitOfWork.Commit();
    }
}