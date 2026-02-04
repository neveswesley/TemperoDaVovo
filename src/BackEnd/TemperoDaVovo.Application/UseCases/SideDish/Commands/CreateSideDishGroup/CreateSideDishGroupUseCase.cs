using System.Text.RegularExpressions;
using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.SideDishGroup.Commands.CreateSideDishGroup;

public class CreateSideDishGroupUseCase : ICreateSideDishGroupUseCase
{
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSideDishGroupUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<CreateSideDishGroupResponseJson> Execute(CreateSideDishGroupRequestJson request)
    {
        await Validate(request);

        var baseName = request.Name;
        var existingName = await _sideDishReadOnlyRepository.GetExistingSideDishNames(request.RestaurantId, baseName);
        var finalName = GenerateSideDishGroupName(baseName, existingName);

        var sideDish = new Domain.Entities.SideDishGroup()
        {
            Name = finalName,
            RestaurantId = request.RestaurantId,
            MinQuantity = request.MinQuantity,
            MaxQuantity = request.MaxQuantity,
            CreatedAt = DateTime.UtcNow,
            IsActive = true
        };
        
        
        
        await _sideDishWriteOnlyRepository.CreateSideDishGroup(sideDish);
        await _unitOfWork.CommitAsync();

        return new CreateSideDishGroupResponseJson()
        {
            Id = sideDish.Id,
            RestaurantId = sideDish.RestaurantId,
            Name = sideDish.Name,
            MinQuantity = sideDish.MinQuantity,
            MaxQuantity = sideDish.MaxQuantity
        };
    }

    private async Task Validate(CreateSideDishGroupRequestJson request)
    {
        var validator = new CreateSideDishGroupValidator();
        var result = await validator.ValidateAsync(request);

        var restaurant = await _restaurantReadOnlyRepository.RestaurantExists(request.RestaurantId);
        
        if (restaurant == false)
            throw new NotFoundException(["Restaurante não encontrado."]);
        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
    private static string GenerateSideDishGroupName(string baseName, List<string> existingNames)
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