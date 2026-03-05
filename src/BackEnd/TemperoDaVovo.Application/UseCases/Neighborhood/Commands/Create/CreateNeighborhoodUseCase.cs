using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.Neighborhood.Commands.Create;

public class CreateNeighborhoodUseCase : ICreateNeighborhoodUseCase
{
    private readonly INeighborhoodWriteOnlyRepository _neighborhoodWriteOnlyRepository;
    private readonly INeighborhoodReadOnlyRepository _neighborhoodReadOnlyRepository;
    private readonly IRestaurantReadOnlyRepository _restaurantReadOnlyRepository;
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateNeighborhoodUseCase(INeighborhoodWriteOnlyRepository neighborhoodWriteOnlyRepository, INeighborhoodReadOnlyRepository neighborhoodReadOnlyRepository, IRestaurantReadOnlyRepository restaurantReadOnlyRepository, ICityReadOnlyRepository cityReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _neighborhoodWriteOnlyRepository = neighborhoodWriteOnlyRepository;
        _neighborhoodReadOnlyRepository = neighborhoodReadOnlyRepository;
        _restaurantReadOnlyRepository = restaurantReadOnlyRepository;
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> Execute(CreateNeighborhoodRequestJson request)
    {
        await Validate(request);
        
        var city = await _cityReadOnlyRepository.GetByIdAsync(request.CityId);
        if (city is null)
            throw new NotFoundException(["Cidade não encontrada."]);

        var neighborhood = new Domain.Entities.Neighborhood(request.Name, request.DeliveryFee, request.CityId, request.BaseDeliveryTimeInMinutes);
        
        await _neighborhoodWriteOnlyRepository.AddAsync(neighborhood);
        await _unitOfWork.CommitAsync();

        return neighborhood.Id;
    }

    private async Task Validate(CreateNeighborhoodRequestJson request)
    {
        var validator = new CreateNeighborhoodValidator();
        var result = await validator.ValidateAsync(request);

        var existingName = await _neighborhoodReadOnlyRepository.ExistingNameByCity(request.Name, request.CityId);

        if (existingName)
            result.Errors.Add(
                new FluentValidation.Results.ValidationFailure(string.Empty, "Este bairro já está cadastrado em seu restaurante."));

        
        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}