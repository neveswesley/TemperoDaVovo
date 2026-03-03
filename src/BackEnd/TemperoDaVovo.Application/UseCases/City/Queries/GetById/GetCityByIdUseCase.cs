using TemperoDaVovo.Communications.Responses;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Queries.GetById;

public class GetCityByIdUseCase : IGetCityByIdUseCase
{
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;

    public GetCityByIdUseCase(ICityReadOnlyRepository cityReadOnlyRepository)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
    }

    public async Task<CityResponseJson> ExecuteAsync(Guid cityId)
    {
        var city = await _cityReadOnlyRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new NotFoundException(["Cidade não encontrada."]);

        var result = new CityResponseJson()
        {
            Id =  city.Id,
            Name = city.Name
        };

        return result;
    }
}