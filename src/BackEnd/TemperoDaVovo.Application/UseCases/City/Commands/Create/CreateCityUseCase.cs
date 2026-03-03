using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Create;

public class CreateCityUseCase : ICreateCityUseCase
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly ICityWriteOnlyRepository _cityWriteOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCityUseCase(ICityReadOnlyRepository cityReadOnlyRepository, ICityWriteOnlyRepository cityWriteOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _cityWriteOnlyRepository = cityWriteOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(CreateCityRequestJson request)
    {
        await ValidateAsync(request);
        
        var restaurant = await _restaurantReadOnlyRepository.GetRestaurantById(request.RestaurantId);
        if (restaurant == null)
            throw new NotFoundException(["Restaurante não encontrado."]);

        var city = new Domain.Entities.City(request.Name, request.RestaurantId);
        
        await _cityWriteOnlyRepository.CreateAsync(city);
        await _unitOfWork.CommitAsync();
        
        return city.Id;
    }

    private async Task ValidateAsync(CreateCityRequestJson request)
    {
        var validator = new CreateCityValidator();
        var result = await validator.ValidateAsync(request);

        if (await _cityReadOnlyRepository.CityExistingByRestaurantId(request.Name, request.RestaurantId))
            result.Errors.Add(new FluentValidation.Results.ValidationFailure(string.Empty,
                "Essa cidade já está cadastrada em seu restaurante."));

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}