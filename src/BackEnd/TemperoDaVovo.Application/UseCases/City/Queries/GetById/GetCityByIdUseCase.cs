using TemperoDaVovo.Application.Interfaces;
using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Queries.GetById;

public class GetCityByIdUseCase : IGetCityByIdUseCase
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly IAuthorizationService _authorizationService;

    public GetCityByIdUseCase(ICityReadOnlyRepository cityReadOnlyRepository, IAuthorizationService authorizationService)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _authorizationService = authorizationService;
    }

    public async Task<CityResponseJson> ExecuteAsync(Guid cityId)
    {
        var city = await _cityReadOnlyRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new NotFoundException(["Cidade não encontrada."]);
        
        _authorizationService.ValidateRestaurantOwnership(city.RestaurantId);

        var result = new CityResponseJson()
        {
            Id =  city.Id,
            Name = city.Name
        };

        return result;
    }
}