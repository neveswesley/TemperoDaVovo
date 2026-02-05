using TemperoDaVovo.Domain.Interfaces.ReadOnly;
using TemperoDaVovo.Domain.Interfaces.WriteOnly;
using TemperoDaVovo.Exceptions.ExceptionsBase;

namespace TemperoDaVovo.Application.UseCases.SideDish.Commands.DeleteSideDish;

public class DeleteSideDishUseCase : IDeleteSideDishUseCase
{
    
    private readonly ISideDishWriteOnlyRepository _sideDishWriteOnlyRepository;
    private readonly ISideDishReadOnlyRepository _sideDishReadOnlyRepository;

    public DeleteSideDishUseCase(ISideDishWriteOnlyRepository sideDishWriteOnlyRepository, ISideDishReadOnlyRepository sideDishReadOnlyRepository)
    {
        _sideDishWriteOnlyRepository = sideDishWriteOnlyRepository;
        _sideDishReadOnlyRepository = sideDishReadOnlyRepository;
    }

    public async Task Execute(Guid sideDishId)
    {
        var sideDish = await _sideDishReadOnlyRepository.GetSideDishById(sideDishId);

        if (sideDish is null)
            throw new NotFoundException(["Complemento não encontrado."]);
        
        await _sideDishWriteOnlyRepository.DeleteSideDish(sideDishId);
    }
}