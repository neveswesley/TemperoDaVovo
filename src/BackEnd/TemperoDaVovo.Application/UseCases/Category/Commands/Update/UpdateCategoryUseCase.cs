using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.UpdateProduct;

public class UpdateCategoryUseCase : IUpdateCategoryUseCase
{

    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<UpdateCategoryResponseJson> Execute(UpdateCategoryRequestJson request, Guid categoryId)
    {
        var category = await _categoryReadOnlyRepository.GetCategoryById(categoryId);
        
        category.UpdateName(request.Name);
        
        await _categoryWriteOnlyRepository.UpdateAsync(category);
        await _unitOfWork.Commit();
        
        return new UpdateCategoryResponseJson()
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
}