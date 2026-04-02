using System.Text.RegularExpressions;
using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Category.Commands.Create;

public class CreateCategoryUseCase : ICreateCategoryUseCase
{
    private readonly ICategoryWriteOnlyRepository _categoryWriteOnlyRepository;
    private readonly ICategoryReadOnlyRepository _categoryReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthorizationService _authorizationService;

    public CreateCategoryUseCase(ICategoryWriteOnlyRepository categoryWriteOnlyRepository, ICategoryReadOnlyRepository categoryReadOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork, IAuthorizationService authorizationService)
    {
        _categoryWriteOnlyRepository = categoryWriteOnlyRepository;
        _categoryReadOnlyRepository = categoryReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
        _authorizationService = authorizationService;
    }

    public async Task<CreateCategoryResponseJson> ExecuteAsync(CreateCategoryRequestJson request)
    {
        
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurant not found."]);
        
        _authorizationService.ValidateRestaurantOwnership(request.RestaurantId);
        
        if (string.IsNullOrEmpty(request.Name))
            throw new ErrorOnValidationException(["Categoria inválida."]);

        var baseName = request.Name.Trim();
        
        var existingNames = await _categoryReadOnlyRepository.GetExistingCategoryNames(request.RestaurantId, baseName);

        var finalName = GenerateCategoryName(baseName, existingNames);
        
        var category = new Domain.Entities.Category()
        {
            RestaurantId = request.RestaurantId,
            Name = finalName,
        };

        await _categoryWriteOnlyRepository.CreateAsync(category);
        await _unitOfWork.CommitAsync();

        return new CreateCategoryResponseJson()
        {
            Id = category.Id,
            
        };
    }
    private static string GenerateCategoryName(string baseName, List<string> existingNames)
    {
        if (!existingNames.Any())
            return baseName;

        var usedNumbers = existingNames
            .Select(name =>
            {
                var match = Regex.Match(name, @"\((\d+)\)$");
                return match.Success
                    ? int.Parse(match.Groups[1].Value)
                    : 0;
            });

        var nextNumber = usedNumbers.Max() + 1;

        return $"{baseName} ({nextNumber})";
    }

}