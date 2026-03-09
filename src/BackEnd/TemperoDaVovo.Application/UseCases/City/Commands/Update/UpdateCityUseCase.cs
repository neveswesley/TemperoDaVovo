using TemperoDaVovo.Communications.Requests;
using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Update;

public class UpdateCityUseCase : IUpdateCityUseCase
{
    
    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly ICityWriteOnlyRepository _cityWriteOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCityUseCase(ICityReadOnlyRepository cityReadOnlyRepository, ICityWriteOnlyRepository cityWriteOnlyRepository, IUnitOfWork unitOfWork)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _cityWriteOnlyRepository = cityWriteOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Guid> ExecuteAsync(Guid cityId, UpdateCityRequestJson request)
    {
        var city = await _cityReadOnlyRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new NotFoundException(["Cidade não encontrada."]);
        
        city.UpdateName(request.Name);
        await _cityWriteOnlyRepository.UpdateAsync(city);
        await _unitOfWork.CommitAsync();
        
        return city.Id;
    }
}