using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Application.UseCases.Category.Commands.Update;
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
    private readonly IAuthorizationService _authorizationService;

    public UpdateCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task<UpdateCategoryResponseJson> ExecuteAsync(UpdateCategoryRequestJson request, Guid categoryId)
    {
        var category = await _categoryReadOnlyRepository.GetCategoryById(categoryId);
        
        _authorizationService.ValidateRestaurantOwnership(category.RestaurantId);
        
        category.UpdateName(request.Name);
        
        await _categoryWriteOnlyRepository.UpdateAsync(category);
        await _unitOfWork.CommitAsync();
        
        return new UpdateCategoryResponseJson()
        {
            Id = category.Id,
            Name = category.Name,
        };
    }
}