using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Delete;

public class DeleteCategoryUseCase : IDeleteCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public DeleteCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task ExecuteAsync(Guid categoryId)
    {
        var category = await _categoryReadOnlyRepository.GetCategoryById(categoryId);
        if (category == null)
            throw new NotFoundException(["Category not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(category.RestaurantId);

        await _categoryWriteOnlyRepository.DeleteAsync(categoryId);
        await _unitOfWork.CommitAsync();
    }
}