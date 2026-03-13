using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.City.Commands.Delete;

public class DeleteCityUseCase : IDeleteCityUseCase
{

    private readonly ICityReadOnlyRepository _cityReadOnlyRepository;
    private readonly ICityWriteOnlyRepository _writeReadOnlyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCityUseCase(ICityReadOnlyRepository cityReadOnlyRepository, ICityWriteOnlyRepository writeReadOnlyRepository, IUnitOfWork unitOfWork)
    {
        _cityReadOnlyRepository = cityReadOnlyRepository;
        _writeReadOnlyRepository = writeReadOnlyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(Guid cityId)
    {
        var city =  await _cityReadOnlyRepository.GetByIdAsync(cityId);
        if (city == null)
            throw new NotFoundException(["Cidade não encontrada."]);
        
        city.Deactivate();
        
        _writeReadOnlyRepository.UpdateAsync(city);
        await _unitOfWork.CommitAsync();
    }
}